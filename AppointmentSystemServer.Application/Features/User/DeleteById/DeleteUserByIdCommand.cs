using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.User.DeleteById;

public record DeleteUserByIdCommand(Guid Id) : IRequest<Result<string>>;