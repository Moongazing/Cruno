namespace Moongazing.Cruno.BuildingBlocks.Domain.Events;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}
