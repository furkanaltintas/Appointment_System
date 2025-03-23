using AppointmentSystemServer.Application.Features.Doctors.Create;
using AppointmentSystemServer.Application.Features.Doctors.GetAll;
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
    public async Task<IActionResult> Create(CreateDoctorCommand createDoctorCommand)
    {
        Result<Unit> result = await _mediator.Send(createDoctorCommand);
        return Ok(result);
    }
}
