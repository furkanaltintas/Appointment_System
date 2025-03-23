using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Departments.Delete;

public record DeleteDepartmentCommand(int Id) : IRequest<Result<Unit>>;