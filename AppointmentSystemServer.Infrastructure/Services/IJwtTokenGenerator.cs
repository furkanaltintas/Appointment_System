using AppointmentSystemServer.Domain.Entities;

namespace AppointmentSystemServer.Infrastructure.Services;

public interface IJwtTokenGenerator
{
    Task<string> CreateToken(AppUser user);
}
