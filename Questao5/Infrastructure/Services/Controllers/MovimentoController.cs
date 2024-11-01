using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Movimentacoes.Commands.Adicionar;
using Questao5.Domain.Shared;
using Questao5.Infrastructure.Services.Controllers.Abstractions;

namespace Questao5.Infrastructure.Services.Controllers
{

    [Route("api/movimento")]

    public class MovimentoController : ApiController
    {
        public MovimentoController(ISender sender)
            : base(sender)
        {
        }



        /// <summary>
        /// NOTA DO DESENVOLVEDOR:
        /// É comum obrigar ao GUID de idempotencia pelo header também 
        /// ex.:  [FromHeader(Name = "X-Idempotencia-Id")] string requset_id,
        ///  Porém isso foi simplificado para apenas servir de exemplo no desafio, pois a funcionalidade é a mesma.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Adicionar(
            [FromBody] AdicionarMovimentacaoCommand request,
            CancellationToken cancellationToken)
        {

            if (request.chave_idempotencia == Guid.Empty)
            {
                return (IActionResult)Results.BadRequest("É necessário adicionar o valor de 'X-Idempotencia-Id:GUID' no header");
            }
            var command = new AdicionarMovimentacaoCommand(
                request.chave_idempotencia,
                request.IdContaCorrente,
                request.Valor,
                request.Tipo);

            try
            {
                var result = await Sender.Send(command, cancellationToken);

                return result.IsSuccess ? Ok(result) : BadRequest(result.Error);

            }
            catch (ArgumentException ex)
            {
               return  BadRequest(ex.Message);
            }
        }
    }
}
