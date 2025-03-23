using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Doctors.Delete;

public record DeleteDoctorCommand(int Id) : IRequest<Result<Unit>>;