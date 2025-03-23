using AppointmentSystemServer.Persistence.Context;
using AppointmentSystemServer.Domain.Entities;
using GenericRepository;
using AppointmentSystemServer.Application.Services.Repositories;

namespace AppointmentSystemServer.Persistence.Repositories;

class DoctorRepository : Repository<Doctor, AppDbContext>, IDoctorRepository
{
    public DoctorRepository(AppDbContext context) : base(context) { }
}
