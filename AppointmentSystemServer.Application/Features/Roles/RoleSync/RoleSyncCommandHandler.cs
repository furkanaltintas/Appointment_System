using AppointmentSystemServer.Application.Features.Roles._Constants;
using AppointmentSystemServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Roles.RoleSync;

class RoleSyncCommandHandler(RoleManager<AppRole> roleManager) : IRequestHandler<RoleSyncCommand, Result<string>>
{
    public async Task<Result<string>> Handle(RoleSyncCommand request, CancellationToken cancellationToken)
    {
        List<AppRole> roles = await roleManager.Roles.ToListAsync(cancellationToken);

        List<AppRole> staticRoles = RoleConstants.GetRoles();

        foreach (AppRole role in roles)
        {
            if (!staticRoles.Contains(role))
                await roleManager.DeleteAsync(role);
        }

        foreach (AppRole role in staticRoles)
        {
            if(!roles.Contains(role))
                await roleManager.CreateAsync(role);
        }

        return RoleConstants.Sync;
    }
}
