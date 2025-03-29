using AppointmentSystemServer.Application.Features.Roles.GetAllRolesForUsers;
using AppointmentSystemServer.Application.Features.Roles.RoleSync;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.WebApi.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TS.Result;

namespace AppointmentSystemServer.WebApi.Controllers;

[AllowAnonymous]
public class RolesController : BaseController
{
    public RolesController(IMediator mediator) : base(mediator) { }


    [HttpGet]
    public async Task<IActionResult> Sync(CancellationToken cancellationToken)
    {
        RoleSyncCommand roleSyncCommand = new();
        Result<string> result = await _mediator.Send(roleSyncCommand, cancellationToken);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken)
    {
        GetAllRoleQuery getAllRolesForUsersQuery = new();
        Result<List<AppRole>> resultListDoctors = await _mediator.Send(getAllRolesForUsersQuery, cancellationToken);
        return StatusCode(resultListDoctors.StatusCode, resultListDoctors);
    }
}