using Moongazing.Cruno.Application.Shared.Abstractions.Messaging;
using Moongazing.Cruno.Modules.Jobs.Domain.Entities;
using Moongazing.Cruno.Modules.Jobs.Infrastructure.Persistence;

namespace Moongazing.Cruno.Modules.Jobs.Application.Features.Jobs.Commands.Create;


public sealed class CreateJobHandler : ICommandHandler<CreateJobCommand, Guid>
{
    private readonly JobsDbContext _dbContext;

    public CreateJobHandler(JobsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        var job = new JobDefinition(
            name: request.Name,
            cronExpression: request.CronExpression,
            payload: request.Payload,
            maxRetryCount: request.MaxRetryCount,
            retryStrategy: request.RetryStrategy
        );

        _dbContext.JobDefinitions.Add(job);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return job.Id;
    }
}
