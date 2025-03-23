using AppointmentSystemServer.Persistence.Context;
using AppointmentSystemServer.Domain.Entities;
using GenericRepository;
using AppointmentSystemServer.Application.Services.Repositories;

namespace AppointmentSystemServer.Persistence.Repositories;

sealed class AppointmentRepository : Repository<Appointment, AppDbContext>, IAppointmentRepository
{
    public AppointmentRepository(AppDbContext context) : base(context) { }
}