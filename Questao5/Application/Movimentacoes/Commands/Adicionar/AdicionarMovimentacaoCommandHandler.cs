using Questao5.Application.Abstractions.Messaging;
using Questao5.Domain.Errors;
using Questao5.Domain.Repositories;
using Questao5.Domain.Shared;

namespace Questao5.Application.Movimentacoes.Commands.Adicionar
{

    internal sealed class AdicionarMovimentacaoCommandHandler :  ICommandHandler<AdicionarMovimentacaoCommand, AdicionarMovimentacaoResponse>
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepositorio;
        private readonly IMovimentoRepository _movimentoRepository;
        

        public AdicionarMovimentacaoCommandHandler(IContaCorrenteRepository contaCorrenteRepositorio, IMovimentoRepository memberRepository)
        {
            _contaCorrenteRepositorio = contaCorrenteRepositorio;   

            _movimentoRepository = memberRepository;
        }

        public async Task<Result<AdicionarMovimentacaoResponse>> Handle(AdicionarMovimentacaoCommand request, CancellationToken cancellationToken)
        {
            if (request.Tipo.ToUpper() != "C" && request.Tipo.ToUpper() != "D")
            {
                return Result.Failure<AdicionarMovimentacaoResponse>(DomainErrors.Movimento.TipoInvalido);
            }
            
            if (request.Valor <= 0)
            {
                return Result.Failure<AdicionarMovimentacaoResponse>(DomainErrors.Movimento.ValorInvalido);
            }

            var contaCorrente = await _contaCorrenteRepositorio.GetByIdAsync(request.IdContaCorrente, cancellationToken);

            if (contaCorrente == null)
            {
                return Result.Failure<AdicionarMovimentacaoResponse>(DomainErrors.ContaCorrente.ContaInvalida);
            }

            if (!contaCorrente.Ativo)
            {
                return Result.Failure<AdicionarMovimentacaoResponse>(DomainErrors.ContaCorrente.ContaInativa);
            }

            var movimentacaoId = await _movimentoRepository.AddAsync(request.IdContaCorrente, request.Valor, request.Tipo.ToUpper(), cancellationToken);

            return new AdicionarMovimentacaoResponse(movimentacaoId);
        }
    }    
}
