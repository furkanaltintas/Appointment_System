using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Commands.DepartmentCommands;

public record CreateDepartmentCommand(string Name) : IRequest<Result<Unit>>;