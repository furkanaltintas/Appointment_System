using AppointmentSystemServer.Domain.Entities;

namespace AppointmentSystemServer.Infrastructure.Services;

public interface IJwtTokenGenerator
{
    string CreateToken(AppUser user);
}
