using Questao5.Application.Abstractions.Messaging;

namespace Questao5.Application.Movimentacoes.Commands.Adicionar
{
    public sealed record AdicionarMovimentacaoCommand(Guid chave_idempotencia, Guid IdContaCorrente, decimal Valor, string Tipo) :  IdempotenciaCommand(chave_idempotencia), ICommand<AdicionarMovimentacaoResponse>;

}
