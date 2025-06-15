using Moongazing.Cruno.Worker.Services.Handlers.Common;
using System.Text.Json;

namespace Moongazing.Cruno.Worker.Services.Handlers;

public class SendEmailJobHandler : IJobHandler
{
    private readonly ILogger<SendEmailJobHandler> _logger;

    public SendEmailJobHandler(ILogger<SendEmailJobHandler> logger)
    {
        _logger = logger;
    }

    public string Action => "send-email";

    public async Task HandleAsync(JsonElement data)
    {
        var to = data.GetProperty("to").GetString();
        var subject = data.GetProperty("subject").GetString();
        var body = data.GetProperty("body").GetString();

        _logger.LogInformation("Sending email to {To} with subject '{Subject}'", to, subject);
        await Task.Delay(500);
        _logger.LogInformation("Email sent to {To}", to);
    }
}
