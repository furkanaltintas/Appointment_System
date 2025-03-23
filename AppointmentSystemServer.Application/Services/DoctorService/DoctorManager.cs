using AppointmentSystemServer.Application.Services.Repositories;
using GenericRepository;

namespace AppointmentSystemServer.Application.Services.DoctorService;

public class DoctorManager(IDoctorRepository doctorRepository, IUnitOfWork uow) : IDoctorService { }