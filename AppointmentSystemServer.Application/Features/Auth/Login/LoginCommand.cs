using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Auth.Login;

public sealed record LoginCommandRequest(string Email, string Password) : IRequest<Result<LoginCommandResponse>>;
