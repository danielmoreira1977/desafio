using Azure.Core;
using MediatR;
using Questao5.Application.Abstractions.Messaging;
using Questao5.Application.Movimentacoes.Commands.Adicionar;
using Questao5.Domain.Errors;

namespace Questao5.Behaviors
{
    internal sealed class IdempotenciaCommandPipelineBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IdempotenciaCommand
    {

        private readonly IIdempotenciaService _idempotenciaService;

        public IdempotenciaCommandPipelineBehavior(IIdempotenciaService idempotenciaService)
        {
            _idempotenciaService = idempotenciaService;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {

            var exists = await _idempotenciaService.RequestExistsAsync(request.chave_idempotencia, cancellationToken);

            if (exists) 
            {
                throw new ArgumentException(DomainErrors.Idempotencia.RequisicaoProcessada);
            }

            await _idempotenciaService.CriarRequestAsync(request.chave_idempotencia, typeof(Request).Name, cancellationToken);
            
            var response = await next();

            return response;
        }
    }
}
