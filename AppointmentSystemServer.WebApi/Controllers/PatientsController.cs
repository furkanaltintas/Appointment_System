using AppointmentSystemServer.Application.Features.Commands.PatientCommands;
using AppointmentSystemServer.Application.Features.Queries.PatientQueries;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.WebApi.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TS.Result;

namespace AppointmentSystemServer.WebApi.Controllers;

public class PatientsController : BaseController
{
    public PatientsController(IMediator mediator) : base(mediator) { }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        GetAllPatientQuery getAllPatientQuery = new();
        Result<List<Patient>> result = await _mediator.Send(getAllPatientQuery, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> GetPatientByIdentityNumber(GetPatientByIdentityNumberQuery getPatientByIdentityNumber, CancellationToken cancellationToken)
    {
        Result<Patient> resultPatients = await _mediator.Send(getPatientByIdentityNumber, cancellationToken);
        return StatusCode(resultPatients.StatusCode, resultPatients);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePatientCommand createPatientCommand)
    {
        Result<string> result = await _mediator.Send(createPatientCommand);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdatePatientCommand updatePatientCommand)
    {
        Result<string> result = await _mediator.Send(updatePatientCommand);
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteById(DeletePatientByIdCommand deletePatientByIdCommand)
    {
        Result<string> result = await _mediator.Send(deletePatientByIdCommand);
        return StatusCode(result.StatusCode, result);
    }
}