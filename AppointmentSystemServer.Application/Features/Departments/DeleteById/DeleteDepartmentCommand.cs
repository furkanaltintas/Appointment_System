using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Departments.DeleteById;

public record DeleteDepartmentCommand(int Id) : IRequest<Result<string>>;