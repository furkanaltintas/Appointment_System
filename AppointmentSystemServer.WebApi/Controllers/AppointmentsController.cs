using AppointmentSystemServer.Application.Features.Appointments.CreateAppointment;
using AppointmentSystemServer.Application.Features.Appointments.DeleteAppointmentById;
using AppointmentSystemServer.Application.Features.Appointments.GetAllAppointments;
using AppointmentSystemServer.Application.Features.Appointments.UpdateAppointment;
using AppointmentSystemServer.WebApi.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TS.Result;

namespace AppointmentSystemServer.WebApi.Controllers
{
    public class AppointmentsController : BaseController
    {
        public AppointmentsController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<IActionResult> GetAllByDoctorId(GetAllAppointmentsQuery getAllAppointmentsQuery, CancellationToken cancellationToken)
        {
            Result<List<GetAllAppointmentsQueryResponse>> resultListGetAllAppointmentsQueryResponse = await _mediator.Send(getAllAppointmentsQuery, cancellationToken);
            return StatusCode(resultListGetAllAppointmentsQueryResponse.StatusCode, resultListGetAllAppointmentsQueryResponse);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAppointmentCommand createAppointmentCommand, CancellationToken cancellationToken)
        {
            Result<string> result = await _mediator.Send(createAppointmentCommand, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateAppointmentCommand updateAppointmentCommand, CancellationToken cancellationToken)
        {
            Result<string> result = await _mediator.Send(updateAppointmentCommand, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteById(DeleteAppointmentByIdCommand deleteAppointmentByIdCommand, CancellationToken cancellationToken)
        {
            Result<string> result = await _mediator.Send(deleteAppointmentByIdCommand, cancellationToken);
            return StatusCode(result.StatusCode, result);
        }
    }
}
