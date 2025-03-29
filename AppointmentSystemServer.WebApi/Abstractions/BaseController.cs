using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentSystemServer.WebApi.Abstractions;

[Route("api/[controller]/[action]")]
[Authorize(AuthenticationSchemes = "Bearer")]
[ApiController]
public class BaseController : ControllerBase
{
    public readonly IMediator _mediator;

    public BaseController(IMediator mediator)
    {
        _mediator = mediator;
    }
}