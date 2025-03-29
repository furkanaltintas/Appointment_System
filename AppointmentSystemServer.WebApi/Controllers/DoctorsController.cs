using AppointmentSystemServer.Application.Features.Commands.DoctorCommands;
using AppointmentSystemServer.Application.Features.Queries.DoctorQueries;
using AppointmentSystemServer.Application.Features.Responses.DoctorResponses;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.WebApi.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TS.Result;

namespace AppointmentSystemServer.WebApi.Controllers;

public class DoctorsController : BaseController
{
    public DoctorsController(IMediator mediator) : base(mediator) { }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        GetAllDoctorQuery getAllDoctorQuery = new();
        Result<List<GetAllDoctorResponse>> result = await _mediator.Send(getAllDoctorQuery, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> GetDoctorsByDepartment(GetAllDoctorByDepartmentQuery getAllDoctorByDepartmentQuery, CancellationToken cancellationToken)
    {
        Result<List<Doctor>> resultListDoctors = await _mediator.Send(getAllDoctorByDepartmentQuery, cancellationToken);
        return StatusCode(resultListDoctors.StatusCode, resultListDoctors);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateDoctorCommand createDoctorCommand)
    {
        Result<string> result = await _mediator.Send(createDoctorCommand);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateDoctorCommand updateDoctorCommand)
    {
        Result<string> result = await _mediator.Send(updateDoctorCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteById(DeleteDoctorByIdCommand deleteDoctorByIdCommand)
    {
        Result<string> result = await _mediator.Send(deleteDoctorByIdCommand);
        return StatusCode(result.StatusCode, result);
    }
}