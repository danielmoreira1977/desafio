namespace Questao5.Domain.Repositories
{
    public interface IIdempotenciaRepository
    {
        Task<bool> RequestExistsAsync(Guid chave_idempotencia, CancellationToken cancellationToken);
        Task CriarRequestAsync(Guid chave_idempotencia, string requisicao,  CancellationToken cancellationToken);


    }
}
