using Dapper;
using Questao5.Domain.Repositories;

namespace Questao5.Infrastructure.Database.Repositories
{
    internal sealed class IdempotenciaRepository : IIdempotenciaRepository
    {

        private readonly IDbSession _session;

        public IdempotenciaRepository(IDbSession session)
        {
            _session = session;
        }

        public async Task CriarRequestAsync(Guid chave_idempotencia, string requisicao, CancellationToken cancellationToken)
        {
            var sql = $"INSERT INTO idempotencia (chave_idempotencia , requisicao) VALUES ('{chave_idempotencia}', '{requisicao}');";

            await _session.Connection.ExecuteAsync(sql);
            
        }

        public async Task<bool> RequestExistsAsync(Guid chave_idempotencia, CancellationToken cancellationToken)
        {
            var sql = $"select count(1) from IDEMPOTENCIA where chave_idempotencia='{chave_idempotencia}';";

            var exists = await _session.Connection.ExecuteScalarAsync<bool>(sql);

            return exists;
        }
    }
}
