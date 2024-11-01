using Dapper;
using Questao5.Domain.Entities;
using Questao5.Domain.Repositories;

namespace Questao5.Infrastructure.Database.Repositories
{
    internal sealed class ContaCorrenteRepository : IContaCorrenteRepository
    {
        private readonly IDbSession _session;

        public ContaCorrenteRepository(IDbSession session)
        {
            _session = session;
        }

        public async Task<ContaCorrente?> GetByIdAsync(Guid contaCorrenteId, CancellationToken cancellationToken)
        {

            var sql =
                @"
                select * from CONTACORRENTE where IdContaCorrente=@id;
                select * from MOVIMENTO where IdContaCorrente=@id;";


            ContaCorrente contaCorrente;

            using (var multi = await _session.Connection.QueryMultipleAsync(sql, new { id = contaCorrenteId}))
            {
                contaCorrente = multi.ReadFirstOrDefault<ContaCorrente>();

                if (contaCorrente != null)
                { 
                    contaCorrente.AddMovimentacao(multi.Read<Movimento>().ToList()); 
                }
            }

            return contaCorrente; 
  
        }

    }
}
