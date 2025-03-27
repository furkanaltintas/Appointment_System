using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Doctors.Update;

public record UpdateDoctorCommand(int Id, int DepartmentId, string FirstName, string LastName) : IRequest<Result<string>>;