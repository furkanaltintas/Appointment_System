using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Commands.UserCommands;

public record DeleteUserByIdCommand(Guid Id) : IRequest<Result<string>>;