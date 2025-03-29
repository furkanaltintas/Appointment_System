using AppointmentSystemServer.Application.Services.Repositories;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Persistence.Context;
using GenericRepository;

namespace AppointmentSystemServer.Persistence.Repositories;

class UserRoleRepository : Repository<AppUserRole, AppDbContext>, IUserRoleRepository
{
    public UserRoleRepository(AppDbContext context) : base(context) { }
}
