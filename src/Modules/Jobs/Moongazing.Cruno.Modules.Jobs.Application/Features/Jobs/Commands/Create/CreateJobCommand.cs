using Moongazing.Cruno.Application.Shared.Abstractions.Messaging;
using Moongazing.Cruno.Modules.Jobs.Domain.Enums;

namespace Moongazing.Cruno.Modules.Jobs.Application.Features.Jobs.Commands.Create;

public sealed record CreateJobCommand(
    string Name,
    string CronExpression,
    string Payload,
    int MaxRetryCount,
    RetryStrategy RetryStrategy
) : ICommand<Guid>;
