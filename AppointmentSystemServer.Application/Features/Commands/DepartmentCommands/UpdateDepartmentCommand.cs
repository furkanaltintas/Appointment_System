using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Commands.DepartmentCommands;

public record UpdateDepartmentCommand(int Id, string Name) : IRequest<Result<string>>;