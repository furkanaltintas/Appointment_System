using AppointmentSystemServer.Application.Features.Departments.GetAll;
using AppointmentSystemServer.WebApi.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TS.Result;

namespace AppointmentSystemServer.WebApi.Controllers;

public class DepartmentsController : BaseController
{
    public DepartmentsController(IMediator mediator) : base(mediator) { }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        GetAllDepartmentQuery getAllDepartmentQuery = new();
        Result<List<GetAllDepartmentResponse>> result = await _mediator.Send(getAllDepartmentQuery, cancellationToken);
        return Ok(result);
    }
}