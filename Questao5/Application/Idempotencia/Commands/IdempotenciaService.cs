using Questao5.Application.Abstractions.Messaging;
using Questao5.Domain.Repositories;

namespace Questao5.Application.Idempotencia.Commands
{
    public sealed class IdempotenciaService : IIdempotenciaService
    {
        private readonly IIdempotenciaRepository _repository;

        public IdempotenciaService(IIdempotenciaRepository repository)
        {
            _repository = repository;
        }

        public async Task CriarRequestAsync(Guid chave_idempotencia, string requisicao,  CancellationToken cancellationToken)
        {
            await _repository.CriarRequestAsync(chave_idempotencia, requisicao,  cancellationToken);
        }

        public async Task<bool> RequestExistsAsync(Guid chave_idempotencia, CancellationToken cancellationToken)
        {
            return await _repository.RequestExistsAsync(chave_idempotencia, cancellationToken);
        }
    }
}
