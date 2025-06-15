using Moongazing.Cruno.Worker.Messaging;

namespace Moongazing.Cruno.Worker.Services;

public interface IJobExecutor
{
    Task ExecuteAsync(JobExecutionMessage message);
}

