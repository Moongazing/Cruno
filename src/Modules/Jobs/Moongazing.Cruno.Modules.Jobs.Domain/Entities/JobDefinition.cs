using Moongazing.Cruno.BuildingBlocks.Domain.Common;
using Moongazing.Cruno.Modules.Jobs.Domain.Enums;

namespace Moongazing.Cruno.Modules.Jobs.Domain.Entities;

public class JobDefinition : Entity<Guid>, IAggregateRoot
{
    public string Name { get; private set; } = null!;
    public string CronExpression { get; private set; } = null!;
    public string Payload { get; private set; } = null!;
    public int MaxRetryCount { get; private set; }
    public RetryStrategy RetryStrategy { get; private set; }
    public bool IsActive { get; private set; } = true;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    private JobDefinition() { }

    public JobDefinition(string name, string cronExpression, string payload, int maxRetryCount, RetryStrategy retryStrategy)
    {
        Id = Guid.NewGuid();
        Name = name;
        CronExpression = cronExpression;
        Payload = payload;
        MaxRetryCount = maxRetryCount;
        RetryStrategy = retryStrategy;
    }

    public void Deactivate() => IsActive = false;
}
