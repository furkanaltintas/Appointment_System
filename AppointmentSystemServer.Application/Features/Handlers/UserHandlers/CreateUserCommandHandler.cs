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
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Handlers.UserHandlers;

class CreateUserCommandHandler(
    UserManager<AppUser> userManager,
    UserBusinessRules userBusinessRules,
    IUserRoleRepository userRoleRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ICacheService cacheService) : IRequestHandler<CreateUserCommand, Result<string>>
{
    public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        Result<string> validate = await userBusinessRules.ValidateAsync(request.Email, request.UserName);
        return validate.IsSuccessful ? await ContinueProcess(request, cancellationToken) : validate;
    }

    private async Task<Result<string>> ContinueProcess(CreateUserCommand request, CancellationToken cancellationToken)
    {
        AppUser appUser = mapper.Map<AppUser>(request);

        IdentityResult identityResult = await userManager.CreateAsync(appUser, request.Password);
        if (!identityResult.Succeeded) return Result<string>.Failure(identityResult.Errors.Select(i => i.Description).ToList());

        if (request.RoleIds.Any())
        {
            List<AppUserRole> appUserRoles = new();
            foreach (var roleId in request.RoleIds)
            {
                AppUserRole appUserRole = new()
                {
                    RoleId = roleId,
                    UserId = appUser.Id
                };
                appUserRoles.Add(appUserRole);
            }

            await userRoleRepository.AddRangeAsync(appUserRoles, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        await cacheService.RemoveAsync(UserConstants.CacheKey);
        return UserConstants.CreateMessage;
    }
}
