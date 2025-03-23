using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AppointmentSystemServer.Persistence.Helpers;
public static class IdentityModelBuilderHelper
{
    public static void IgnoreIdentityEntities(ModelBuilder builder)
    {
        var identityEntities = new[]
        {
            typeof(IdentityUserClaim<Guid>),
            typeof(IdentityRoleClaim<Guid>),
            typeof(IdentityUserLogin<Guid>),
            typeof(IdentityUserToken<Guid>)
        };

        foreach (var identityEntity in identityEntities)
            builder.Ignore(identityEntity);
    }
}