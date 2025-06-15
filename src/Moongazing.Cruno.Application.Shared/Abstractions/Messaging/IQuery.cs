using MediatR;

namespace Moongazing.Cruno.Application.Shared.Abstractions.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse> { }
