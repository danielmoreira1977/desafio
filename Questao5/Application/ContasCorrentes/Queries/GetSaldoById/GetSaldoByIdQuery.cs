using Questao5.Application.Abstractions.Messaging;

namespace Questao5.Application.ContasCorrentes.Queries.GetSaldoById
{
    public sealed record GetSaldoByIdQuery(Guid contaCorrenteId) : IQuery<GetSaldoByIdResponse>;


}
