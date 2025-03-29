using AppointmentSystemServer.Application.Features.Responses.AuthResponses;
using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Commands.AuthCommands;

public sealed record LoginCommandRequest(string Email, string Password) : IRequest<Result<LoginCommandResponse>>;
