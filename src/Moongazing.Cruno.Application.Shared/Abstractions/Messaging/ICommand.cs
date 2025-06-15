using MediatR;

namespace Moongazing.Cruno.Application.Shared.Abstractions.Messaging;

public interface ICommand<out TResponse> : IRequest<TResponse> { }
