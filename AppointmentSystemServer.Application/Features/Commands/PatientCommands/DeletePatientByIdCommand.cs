using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Commands.PatientCommands;

public record DeletePatientByIdCommand(string Id) : IRequest<Result<string>>;