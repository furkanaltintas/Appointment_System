using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Roles.RoleSync;

public record RoleSyncCommand() : IRequest<Result<string>>;