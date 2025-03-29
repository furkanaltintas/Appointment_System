using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Commands.UserCommands;

public record UpdateUserCommand(
    Guid Id,
        string FirstName,
    string LastName,
    string Email,
    string UserName,
    List<Guid> RoleIds) :  IRequest<Result<string>>;