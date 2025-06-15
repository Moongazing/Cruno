using MediatR;

namespace Moongazing.Cruno.Application.Shared.Abstractions.Messaging;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : IRequest<TResponse>
{
}