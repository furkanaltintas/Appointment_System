using AppointmentSystemServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Queries.RoleQueries;

public record GetAllRoleQuery() : IRequest<Result<List<AppRole>>>;
