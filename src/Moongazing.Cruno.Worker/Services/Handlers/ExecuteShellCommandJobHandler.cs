using Moongazing.Cruno.Worker.Services.Handlers.Common;
using System.Diagnostics;
using System.Text.Json;

namespace Moongazing.Cruno.Worker.Services.Handlers;

public class ExecuteShellCommandJobHandler : IJobHandler
{
    private readonly ILogger<ExecuteShellCommandJobHandler> _logger;

    public ExecuteShellCommandJobHandler(ILogger<ExecuteShellCommandJobHandler> logger)
    {
        _logger = logger;
    }

    public string Action => "exec-shell";

    public async Task HandleAsync(JsonElement data)
    {
        string? command = data.GetProperty("command").GetString();
        string? arguments = data.TryGetProperty("arguments", out var argEl) ? argEl.GetString() : null;

        if (string.IsNullOrWhiteSpace(command))
            throw new InvalidOperationException("Missing 'command'");

        _logger.LogInformation("Executing shell command: {Command} {Args}", command, arguments);

        using var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = command,
                Arguments = arguments ?? "",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        process.Start();

        string output = await process.StandardOutput.ReadToEndAsync();
        string error = await process.StandardError.ReadToEndAsync();

        await process.WaitForExitAsync();

        if (process.ExitCode != 0)
            throw new Exception($"Command failed with exit code {process.ExitCode}. Error: {error}");

        _logger.LogInformation("Shell command output:\n{Output}", output);
    }
}