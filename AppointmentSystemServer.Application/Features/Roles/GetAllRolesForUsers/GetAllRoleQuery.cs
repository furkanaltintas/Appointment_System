using AppointmentSystemServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Roles.GetAllRolesForUsers;

public record GetAllRoleQuery() : IRequest<Result<List<AppRole>>>;
