using FluentValidation;

namespace Moongazing.Cruno.Modules.Jobs.Application.Features.Jobs.Commands.Create;

public class CreateJobValidator : AbstractValidator<CreateJobCommand>
{
    public CreateJobValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.CronExpression).NotEmpty();
        RuleFor(x => x.Payload).NotEmpty();
        RuleFor(x => x.MaxRetryCount).GreaterThanOrEqualTo(0).LessThanOrEqualTo(10);
        RuleFor(x => x.RetryStrategy).IsInEnum();
    }
}
