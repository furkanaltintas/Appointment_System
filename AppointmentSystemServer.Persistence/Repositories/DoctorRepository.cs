using AppointmentSystemServer.Application.Services.Repositories;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Persistence.Context;
using GenericRepository;

namespace AppointmentSystemServer.Persistence.Repositories;

class DoctorRepository : Repository<Doctor, AppDbContext>, IDoctorRepository
{
    public DoctorRepository(AppDbContext context) : base(context) { }
}
