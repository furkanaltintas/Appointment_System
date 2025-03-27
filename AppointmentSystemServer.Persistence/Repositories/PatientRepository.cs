using AppointmentSystemServer.Application.Services.Repositories;
using AppointmentSystemServer.Domain.Entities;
using AppointmentSystemServer.Persistence.Context;
using GenericRepository;

namespace AppointmentSystemServer.Persistence.Repositories;

class PatientRepository : Repository<Patient, AppDbContext>, IPatientRepository
{
    public PatientRepository(AppDbContext context) : base(context) { }
}