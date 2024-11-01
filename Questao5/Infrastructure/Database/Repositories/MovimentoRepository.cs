using Dapper;
using Questao5.Domain.Repositories;

namespace Questao5.Infrastructure.Database.Repositories
{
    internal sealed class MovimentoRepository : IMovimentoRepository
    {


        private readonly IDbSession _session;

        public MovimentoRepository(IDbSession session)
        {
            _session = session;
        }
        public async Task<string?> AddAsync(Guid contaCorrenteId, decimal valor, string tipo, CancellationToken cancellationToken)
        {
            var movimentacaoId = Guid.NewGuid().ToString();

            var sql = @"
            INSERT INTO [movimento] VALUES 
                (
                    @idmovimento,
                    @idcontacorrente,
                    @datamovimento, 
                    @tipomovimento, 
                    @valor
            );";

            await _session.Connection.ExecuteAsync(sql, 
                new 
                { 
                    idmovimento = movimentacaoId,
                    idcontacorrente = contaCorrenteId,
                    datamovimento = DateTime.UtcNow,
                    tipomovimento = tipo.ToUpper(),
                    valor = valor
                });

            return movimentacaoId;
        }
    }
}
