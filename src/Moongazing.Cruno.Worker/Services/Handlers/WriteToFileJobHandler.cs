using Moongazing.Cruno.Worker.Services.Handlers.Common;
using System.Text.Json;

namespace Moongazing.Cruno.Worker.Services.Handlers;

public class WriteToFileJobHandler : IJobHandler
{
    private readonly ILogger<WriteToFileJobHandler> _logger;

    public WriteToFileJobHandler(ILogger<WriteToFileJobHandler> logger)
    {
        _logger = logger;
    }

    public string Action => "write-to-file";

    public async Task HandleAsync(JsonElement data)
    {
        string? path = data.GetProperty("path").GetString();
        string? content = data.GetProperty("content").GetString();

        if (string.IsNullOrWhiteSpace(path))
            throw new InvalidOperationException("Missing 'path'");

        if (string.IsNullOrWhiteSpace(content))
            throw new InvalidOperationException("Missing 'content'");

        var directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrWhiteSpace(directory))
            Directory.CreateDirectory(directory);

        await File.AppendAllTextAsync(path, content + Environment.NewLine);
        _logger.LogInformation("Content written to file: {Path}", path);
    }
}