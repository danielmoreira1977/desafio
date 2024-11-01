using Questao5.Application.Abstractions.Messaging;
using Questao5.Domain.Errors;
using Questao5.Domain.Repositories;
using Questao5.Domain.Shared;

namespace Questao5.Application.ContasCorrentes.Queries.GetSaldoById
{


    internal sealed class GetSaldoByIdQueryhandler
    : IQueryHandler<GetSaldoByIdQuery, GetSaldoByIdResponse>
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;

        public GetSaldoByIdQueryhandler(IContaCorrenteRepository memberRepository)
        {
            _contaCorrenteRepository = memberRepository;
        }

        public async Task<Result<GetSaldoByIdResponse>> Handle(
            GetSaldoByIdQuery request,
            CancellationToken cancellationToken)
        {
            var contaCorrente = await _contaCorrenteRepository.GetByIdAsync(request.contaCorrenteId,cancellationToken);

            if (contaCorrente is null)
            {
                return Result.Failure<GetSaldoByIdResponse>(DomainErrors.ContaCorrente.ContaInvalida);
            }

            if (!contaCorrente.Ativo)
            {
                return Result.Failure<GetSaldoByIdResponse>(DomainErrors.ContaCorrente.ContaInativa);
            }

            var response = new GetSaldoByIdResponse(contaCorrente.IdContaCorrente, contaCorrente.Nome, contaCorrente.Numero, contaCorrente.Saldo.ToString("F"), DateTime.UtcNow);

            return response;
        }
    }

}
