using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Doctors.DeleteById;

public record DeleteDoctorByIdCommand(string Id) : IRequest<Result<string>>;