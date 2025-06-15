using Moongazing.Cruno.Worker.Services.Handlers.Common;
using System.Text;
using System.Text.Json;

namespace Moongazing.Cruno.Worker.Services.Handlers;

public class CallWebhookJobHandler : IJobHandler
{
    private readonly ILogger<CallWebhookJobHandler> _logger;
    private readonly HttpClient _httpClient;

    public CallWebhookJobHandler(ILogger<CallWebhookJobHandler> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClient = httpClientFactory.CreateClient("WebhookClient");
    }

    public string Action => "call-webhook";

    public async Task HandleAsync(JsonElement data)
    {
        if (!data.TryGetProperty("url", out var urlElement) || urlElement.ValueKind != JsonValueKind.String)
            throw new InvalidOperationException("Missing or invalid 'url'");

        string url = urlElement.GetString()!;
        if (!data.TryGetProperty("payload", out var payloadElement))
            throw new InvalidOperationException("Missing 'payload'");

        string jsonPayload = JsonSerializer.Serialize(payloadElement);
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        _logger.LogInformation("Sending webhook to: {Url}", url);

        var response = await _httpClient.PostAsync(url, content);

        if (!response.IsSuccessStatusCode)
        {
            var body = await response.Content.ReadAsStringAsync();
            throw new Exception($"Webhook failed. Status: {response.StatusCode}, Body: {body}");
        }

        _logger.LogInformation("Webhook sent successfully to {Url}", url);
    }
}