using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Commands.UserCommands;

public record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string UserName,
    string Password,
    List<Guid> RoleIds) : IRequest<Result<string>>;