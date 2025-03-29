using AppointmentSystemServer.Application.Features.Commands.UserCommands;
using AppointmentSystemServer.Application.Features.Queries.UserQueries;
using AppointmentSystemServer.Application.Features.Responses.UserResponses;
using AppointmentSystemServer.WebApi.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TS.Result;

namespace AppointmentSystemServer.WebApi.Controllers;

[AllowAnonymous]
public class UsersController : BaseController
{
    public UsersController(IMediator mediator) : base(mediator) { }


    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        GetAllUserQuery getAllUserQuery = new();
        Result<List<GetAllUserQueryResponse>> result = await _mediator.Send(getAllUserQuery, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserCommand createUserCommand)
    {
        Result<string> result = await _mediator.Send(createUserCommand);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateUserCommand updateUserCommand)
    {
        Result<string> result = await _mediator.Send(updateUserCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteById(DeleteUserByIdCommand deleteUserByIdCommand)
    {
        Result<string> result = await _mediator.Send(deleteUserByIdCommand);
        return StatusCode(result.StatusCode, result);
    }
}
