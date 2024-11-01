using MediatR;
using Questao5.Domain.Shared;

namespace Questao5.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}