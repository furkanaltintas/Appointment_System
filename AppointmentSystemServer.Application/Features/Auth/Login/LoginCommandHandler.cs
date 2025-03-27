using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Auth.Login;

sealed class LoginCommandHandler(UserManager<AppUser> userManager, IJwtTokenGenerator jwtTokenGenerator) : IRequestHandler<LoginCommandRequest, Result<LoginCommandResponse>>
{
    public async Task<Result<LoginCommandResponse>> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager.Users.FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);
        if (user is null) return Result<LoginCommandResponse>.Failure("User not found");

        bool checkPassword = await userManager.CheckPasswordAsync(user, request.Password);
        if (!checkPassword) return Result<LoginCommandResponse>.Failure("Email or password is incorrect");

        string token = jwtTokenGenerator.CreateToken(user);
        LoginCommandResponse loginCommandResponse = new(token);
        return Result<LoginCommandResponse>.Succeed(loginCommandResponse);
    }
}