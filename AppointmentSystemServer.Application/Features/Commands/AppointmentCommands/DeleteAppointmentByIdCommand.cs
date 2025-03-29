using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Commands.AppointmentCommands;

public record DeleteAppointmentByIdCommand(int Id, int DoctorId) : IRequest<Result<string>>;
