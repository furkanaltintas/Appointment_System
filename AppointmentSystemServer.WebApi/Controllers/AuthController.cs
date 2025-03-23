using AppointmentSystemServer.Application.Features.Auth.Login;
using AppointmentSystemServer.WebApi.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TS.Result;

namespace AppointmentSystemServer.WebApi.Controllers;


public sealed class AuthController : BaseController
{
    public AuthController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    public async Task<IActionResult> Login(LoginCommandRequest loginCommandRequest, CancellationToken cancellationToken)
    {   
        Result<LoginCommandResponse> result = await _mediator.Send(loginCommandRequest, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }
}