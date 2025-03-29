using AppointmentSystemServer.Application.Features.Constants.UserConstants;
using AppointmentSystemServer.Application.Features.Queries.UserQueries;
using AppointmentSystemServer.Application.Features.Responses.UserResponses;
using AppointmentSystemServer.Application.Services.Repositories;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Infrastructure.Caching;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Handlers.UserHandlers;

class GetAllUserQueryHandler(
    UserManager<AppUser> userManager,
    RoleManager<AppRole> roleManager,
    IUserRoleRepository userRoleRepository,
    IMapper mapper,
    ICacheService cacheService) : IRequestHandler<GetAllUserQuery, Result<List<GetAllUserQueryResponse>>>
{
    public async Task<Result<List<GetAllUserQueryResponse>>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        List<AppUser> appUsers = await userManager.Users.OrderBy(u => u.FirstName).ThenBy(u => u.LastName).ToListAsync(cancellationToken);

        return await cacheService.GetOrSetAsync(UserConstants.CacheKey, async () =>
        await ListUserRolesAsync(mapper.Map<List<GetAllUserQueryResponse>>(appUsers), cancellationToken)
        );
    }

    private async Task<List<GetAllUserQueryResponse>> ListUserRolesAsync(List<GetAllUserQueryResponse> getAllUsersQueryResponses, CancellationToken cancellationToken)
    {
        foreach (var getAllUsersQueryResponse in getAllUsersQueryResponses)
        {
            List<AppUserRole> appUserRoles = await userRoleRepository.Where(u => u.UserId == getAllUsersQueryResponse.Id).ToListAsync(cancellationToken);

            List<Guid> guidRoles = new();
            List<string?> stringRoleNames = new();

            foreach (var appUserRole in appUserRoles)
            {
                AppRole? appRole = await roleManager.Roles.Where(r => r.Id == appUserRole.RoleId).FirstOrDefaultAsync(cancellationToken);

                if (appRole is not null)
                {
                    guidRoles.Add(appRole.Id);
                    stringRoleNames.Add(appRole.Name);
                }
            }

            getAllUsersQueryResponse.RoleIds = guidRoles;
            getAllUsersQueryResponse.RoleNames = stringRoleNames;
        }

        return getAllUsersQueryResponses;
    }
}



//List<GetAllUserQueryResponse> getAllUserQueryResponses = appUsers.Select(a => new GetAllUserQueryResponse()
//{
//    Id = a.Id,
//    FirstName = a.FirstName,
//    LastName = a.LastName,
//    FullName = a.FullName,
//    UserName = a.UserName,
//    Email = a.Email
//}).ToList();
