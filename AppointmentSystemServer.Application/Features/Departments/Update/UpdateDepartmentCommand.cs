using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Departments.Update;

public record UpdateDepartmentCommand(int Id, string Name) : IRequest<Result<string>>;