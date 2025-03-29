using AppointmentSystemServer.Application.Features.Constants.RoleConstants;
using AppointmentSystemServer.Application.Features.Queries.RoleQueries;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Infrastructure.Caching;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Handlers.RoleHandlers;

class GetAllRoleQueryHandler(
    RoleManager<AppRole> roleManager,
    ICacheService cacheService) : IRequestHandler<GetAllRoleQuery, Result<List<AppRole>>>
{
    public async Task<Result<List<AppRole>>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
    {
        return await cacheService.GetOrSetAsync(RoleConstants.CacheKey, async () => 
        await roleManager.Roles.OrderBy(r => r.Name).ToListAsync(cancellationToken));
    }
}
