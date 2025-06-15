using System.Text.Json;

namespace Moongazing.Cruno.Worker.Services.Handlers.Common;

public interface IJobHandler
{
    string Action { get; }
    Task HandleAsync(JsonElement data);
}
