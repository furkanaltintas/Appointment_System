using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Appointments.DeleteById;

public record DeleteAppointmentByIdCommand(int Id, int DoctorId) : IRequest<Result<string>>;
