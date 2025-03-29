using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Commands.DoctorCommands;

public record DeleteDoctorByIdCommand(string Id) : IRequest<Result<string>>;