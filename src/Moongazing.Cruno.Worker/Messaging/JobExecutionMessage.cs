using System.Text.Json;

namespace Moongazing.Cruno.Worker.Messaging;

public class JobExecutionMessage
{
    public Guid JobId { get; set; }
    public string Payload { get; set; } = default!;
    public int RetryCount { get; set; }
    public string RetryStrategy { get; set; } = "None"; 
}
public class JobPayload
{
    public string Action { get; set; } = default!;
    public JsonElement Data { get; set; }
}