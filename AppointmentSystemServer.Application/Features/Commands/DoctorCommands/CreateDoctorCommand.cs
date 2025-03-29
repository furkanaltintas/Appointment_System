using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Commands.DoctorCommands;

public record class CreateDoctorCommand(string FirstName, string LastName, int DepartmentId) : IRequest<Result<string>>;