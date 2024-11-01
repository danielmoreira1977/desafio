using MediatR;
using Questao5.Domain.Shared;

namespace Questao5.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{
}


public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
