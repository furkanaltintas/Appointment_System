using AppointmentSystemServer.Persistence.Context;
using AppointmentSystemServer.Domain.Entities;
using GenericRepository;
using AppointmentSystemServer.Application.Services.Repositories;

namespace AppointmentSystemServer.Persistence.Repositories;

class PatientRepository : Repository<Patient, AppDbContext>, IPatientRepository
{
    public PatientRepository(AppDbContext context) : base(context) { }
}