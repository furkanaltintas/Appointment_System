using AppointmentSystemServer.Domain.Entities;
using GenericRepository;

namespace AppointmentSystemServer.Application.Services.Repositories;

public interface IAppointmentRepository : IRepository<Appointment> { }