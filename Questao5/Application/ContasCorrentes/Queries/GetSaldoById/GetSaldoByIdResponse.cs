namespace Questao5.Application.ContasCorrentes.Queries.GetSaldoById
{
    public sealed record GetSaldoByIdResponse(string IdContaCorrente, string Titular, int Numero, string Saldo, DateTime data);
}
