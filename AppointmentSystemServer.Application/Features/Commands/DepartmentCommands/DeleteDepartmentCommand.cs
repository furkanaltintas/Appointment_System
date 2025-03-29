using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Commands.DepartmentCommands;

public record DeleteDepartmentCommand(int Id) : IRequest<Result<string>>;