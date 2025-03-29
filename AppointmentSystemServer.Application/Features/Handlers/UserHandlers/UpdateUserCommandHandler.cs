using AppointmentSystemServer.Application.Commons;
using AppointmentSystemServer.Application.Features.Commands.UserCommands;
using AppointmentSystemServer.Application.Features.Constants.UserConstants;
using AppointmentSystemServer.Application.Features.Rules;
using AppointmentSystemServer.Application.Services.Repositories;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Infrastructure.Caching;
using AutoMapper;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Handlers.UserHandlers;

class UpdateUserCommandHandler(
    UserManager<AppUser> userManager,
    UserBusinessRules userBusinessRules,
    IUserRoleRepository userRoleRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ICacheService cacheService) : IRequestHandler<UpdateUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        Result<string> result = ResultValidate.Run(
            await userBusinessRules.NotFoundAsync(request.Id),
            await userBusinessRules.ValidateAsync(request.Email, request.UserName)
            );

        return result.IsSuccessful ? await ContinueProcess(request, cancellationToken) : result;
    }


    private async Task<Result<string>> ContinueProcess(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager.FindByIdAsync(request.Id.ToString());
        mapper.Map(request, user);

        IdentityResult identityResult = await userManager.UpdateAsync(user);
        if (!identityResult.Succeeded) return Result<string>.Failure(identityResult.Errors.Select(i => i.Description).ToList());

        if (request.RoleIds.Any())
        {
            List<AppUserRole> appUserRoles = await userRoleRepository.Where(u => u.UserId == user.Id).ToListAsync();
            userRoleRepository.DeleteRange(appUserRoles);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            appUserRoles = new();
            foreach (var roleId in request.RoleIds)
            {
                AppUserRole appUserRole = new()
                {
                    RoleId = roleId,
                    UserId = user.Id
                };
                appUserRoles.Add(appUserRole);
            }

            await userRoleRepository.AddRangeAsync(appUserRoles, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        await cacheService.RemoveAsync(UserConstants.CacheKey);
        return UserConstants.UpdateMessage;
    }
}
