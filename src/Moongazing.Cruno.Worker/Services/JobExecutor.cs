using Moongazing.Cruno.Worker.Messaging;
using Moongazing.Cruno.Worker.Services.Handlers.Common;
using System.Text.Json;

namespace Moongazing.Cruno.Worker.Services;

public class JobExecutor : IJobExecutor
{
    private readonly ILogger<JobExecutor> _logger;
    private readonly IEnumerable<IJobHandler> _handlers;

    public JobExecutor(ILogger<JobExecutor> logger, IEnumerable<IJobHandler> handlers)
    {
        _logger = logger;
        _handlers = handlers;
    }

    public async Task ExecuteAsync(JobExecutionMessage message)
    {
        int maxRetry = message.RetryCount;
        string retryStrategy = message.RetryStrategy ?? "None";

        int attempt = 0;
        bool success = false;

        while (attempt <= maxRetry && !success)
        {
            try
            {
                _logger.LogInformation("Attempt {Attempt} for Job {JobId}", attempt + 1, message.JobId);

                var payload = JsonSerializer.Deserialize<JobPayload>(message.Payload) ?? throw new InvalidOperationException("Invalid payload.");
                var handler = _handlers.FirstOrDefault(h => h.Action == payload.Action) ?? throw new NotSupportedException($"No handler for action '{payload.Action}'");
                await handler.HandleAsync(payload.Data);

                _logger.LogInformation("Job {JobId} completed successfully on attempt {Attempt}", message.JobId, attempt + 1);
                success = true;
            }
            catch (Exception ex)
            {
                attempt++;
                _logger.LogWarning(ex, "Job {JobId} failed on attempt {Attempt}", message.JobId, attempt);

                if (attempt > maxRetry)
                {
                    _logger.LogError("Job {JobId} failed after {MaxRetry} attempts.", message.JobId, maxRetry);
                    break;
                }

                var delay = GetRetryDelay(retryStrategy, attempt);
                _logger.LogInformation("Retrying job {JobId} in {Delay}ms", message.JobId, delay.TotalMilliseconds);
                await Task.Delay(delay);
            }
        }
    }

    private TimeSpan GetRetryDelay(string strategy, int attempt)
    {
        return strategy.ToLowerInvariant() switch
        {
            "linear" => TimeSpan.FromSeconds(2 * attempt),
            "exponential" => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
            _ => TimeSpan.Zero
        };
    }
}

