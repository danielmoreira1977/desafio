using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.ContasCorrentes.Queries.GetSaldoById;
using Questao5.Domain.Shared;
using Questao5.Infrastructure.Services.Controllers.Abstractions;

namespace Questao5.Infrastructure.Services.Controllers
{
    [Route("api/conta-corrente")]

    public class ContaCorrenteController : ApiController
    {
        public ContaCorrenteController(ISender sender)
            : base(sender)
        {
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetSaldoById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetSaldoByIdQuery(id);

            Result<GetSaldoByIdResponse> response = await Sender.Send(query, cancellationToken);

            return response.IsSuccess ? Ok(response.Value) : BadRequest(response.Error);
        }
    }
}
