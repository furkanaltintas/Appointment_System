using AppointmentSystemServer.Application.Features.Responses.DepartmentResponses;
using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Queries.DepartmentQueries;

public sealed record GetAllDepartmentQuery() : IRequest<Result<List<GetAllDepartmentResponse>>>;