namespace Questao5.Domain.Repositories
{
    public interface IMovimentoRepository
    {
        Task<string?> AddAsync(Guid contaCorrenteId, decimal valor, string tipo, CancellationToken cancellationToken);
    }
}
