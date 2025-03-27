using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Patients.DeleteById;

public record DeletePatientByIdCommand(string Id) : IRequest<Result<string>>;