using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Appointments.Update;

public record UpdateAppointmentCommand(
    int Id,
    string StartDate,
    string EndDate) : IRequest<Result<string>>;
