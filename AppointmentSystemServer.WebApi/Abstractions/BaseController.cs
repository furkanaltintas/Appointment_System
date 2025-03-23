using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentSystemServer.WebApi.Abstractions;

[Route("api/[controller]/[action]")]
[ApiController]
public class BaseController : ControllerBase
{
    public readonly IMediator _mediator;

    public BaseController(IMediator mediator)
    {
        _mediator = mediator;
    }      
}