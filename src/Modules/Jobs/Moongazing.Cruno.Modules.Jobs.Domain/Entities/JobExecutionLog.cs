using Moongazing.Cruno.BuildingBlocks.Domain.Common;

namespace Moongazing.Cruno.Modules.Jobs.Domain.Entities;

public class JobExecutionLog : Entity<Guid>
{
    public Guid JobId { get; private set; }
    public DateTime ExecutedAt { get; private set; } = DateTime.UtcNow;
    public bool Success { get; private set; }
    public string? ErrorMessage { get; private set; }
    public int RetryCount { get; private set; }
    public TimeSpan Duration { get; private set; }

    private JobExecutionLog() { }

    public JobExecutionLog(Guid jobId, bool success, string? errorMessage, int retryCount, TimeSpan duration)
    {
        Id = Guid.NewGuid();
        JobId = jobId;
        Success = success;
        ErrorMessage = errorMessage;
        RetryCount = retryCount;
        Duration = duration;
    }
}
