using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Doctors.Update;

public record UpdateDoctorCommand(int Id, int DepartmantId, string FirstName, string LastName) : IRequest<Result<Unit>>;