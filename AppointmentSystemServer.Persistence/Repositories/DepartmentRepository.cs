using AppointmentSystemServer.Application.Services.Repositories;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Persistence.Context;
using GenericRepository;

namespace AppointmentSystemServer.Persistence.Repositories;

class DepartmentRepository : Repository<Department, AppDbContext>, IDepartmentRepository
{
    public DepartmentRepository(AppDbContext context) : base(context) { }
}