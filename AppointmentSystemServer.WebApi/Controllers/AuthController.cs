using AppointmentSystemServer.Application.Features.Commands.AuthCommands;
using AppointmentSystemServer.Application.Features.Responses.AuthResponses;
using AppointmentSystemServer.WebApi.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TS.Result;

namespace AppointmentSystemServer.WebApi.Controllers;

[AllowAnonymous]
public class AuthController : BaseController
{
    public AuthController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    public async Task<IActionResult> Login(LoginCommandRequest loginCommandRequest, CancellationToken cancellationToken)
    {
        Result<LoginCommandResponse> result = await _mediator.Send(loginCommandRequest, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }
}