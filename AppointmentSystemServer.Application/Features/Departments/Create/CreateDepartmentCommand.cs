using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Departments.Create;

public record CreateDepartmentCommand(string Name) : IRequest<Result<Unit>>;