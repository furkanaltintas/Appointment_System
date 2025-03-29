using AppointmentSystemServer.Application.Features.Users._Constants;
using AppointmentSystemServer.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.User._Rules;

public class UserBusinessRules(UserManager<AppUser> userManager)
{
    public async Task<Result<string>> ValidateAsync(string email, string userName)
    {
        Result<string> emailResult = await EmailAlreadyExists(email);
        if (!emailResult.IsSuccessful) return emailResult;

        Result<string> userResult = await UserNameAlreadyExists(userName);
        if (!userResult.IsSuccessful) return userResult;

        return String.Empty;
    }
    public async Task<Result<string>> NotFoundAsync(Guid id)
    {
        AppUser? appUser = await userManager.FindByIdAsync(id.ToString());
        if (appUser is null) return Result<string>.Failure(UserConstants.NotFound);
        return String.Empty;
    }
    public async Task<Result<string>> NotDeleteAsync(Guid id)
    {
        AppUser? appUser = await userManager.FindByIdAsync(id.ToString());
        IdentityResult identityResult = await userManager.DeleteAsync(appUser);
        if (!identityResult.Succeeded) return Result<string>.Failure(identityResult.Errors.Select(i => i.Description).ToList());
        return String.Empty;
    }



    // PRIVATE
    private async Task<Result<string>> EmailAlreadyExists(string email)
    {
        Boolean isExists = await userManager.Users.AnyAsync(u => u.Email == email);

        return isExists
            ? Result<string>.Failure(UserConstants.EmailAlreadyExists)
            : String.Empty;
    }
    private async Task<Result<string>> UserNameAlreadyExists(string userName)
    {
        Boolean isExists = await userManager.Users.AnyAsync(u => u.UserName == userName);

        return isExists
            ? Result<string>.Failure(UserConstants.UserNameAlreadyExists)
            : String.Empty;
    }  
}