using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Doctors.Create;

public record class CreateDoctorCommand(string FirstName, string LastName, int DepartmentId) : IRequest<Result<string>>;