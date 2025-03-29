using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.User.Create;

public record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string UserName,
    string Password,
    List<Guid> RoleIds) : IRequest<Result<string>>;