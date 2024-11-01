namespace Questao5.Application.Abstractions.Messaging
{
    public interface IIdempotenciaService
    {
        Task<bool> RequestExistsAsync(Guid chave_idempotencia, CancellationToken cancellationToken);
        Task CriarRequestAsync(Guid chave_idempotencia, string requisicao, CancellationToken cancellationToken);
    }
}
