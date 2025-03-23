using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Departments.GetAll;

public sealed record GetAllDepartmentQuery() : IRequest<Result<List<GetAllDepartmentResponse>>>;