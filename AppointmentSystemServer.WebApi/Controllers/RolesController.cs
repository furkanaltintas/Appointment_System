using AppointmentSystemServer.Application.Features.Queries.RoleQueries;
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
    public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken)
    {
        GetAllRoleQuery getAllRolesForUsersQuery = new();
        Result<List<AppRole>> resultListDoctors = await _mediator.Send(getAllRolesForUsersQuery, cancellationToken);
        return StatusCode(resultListDoctors.StatusCode, resultListDoctors);
    }
}