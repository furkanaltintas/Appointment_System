using AppointmentSystemServer.Domain.Entities;

namespace AppointmentSystemServer.Application.Features.Constants.RoleConstants;

public static class RoleConstants
{
    public const string CacheKey = "Role:GetAll";
    public const string Sync = "Sync is successful";

    public static List<AppRole> GetRoles()
    {
        List<string> roles = new() {"Admin", "Doctor", "Staff" };
        return roles.Select(r => new AppRole() { Name = r }).ToList();
    }
}