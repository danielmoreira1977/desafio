using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Questao5.Infrastructure.Services.Controllers.Abstractions;

[ApiController]
public abstract class ApiController : ControllerBase
{
    protected readonly ISender Sender;

    protected ApiController(ISender sender)
    {
        Sender = sender;
    }
}