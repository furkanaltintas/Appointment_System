using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Patients.Create;

public record CreatePatientCommand(
    string FirstName,
    string LastName,
    string IdentityNumber,
    string City,
    string Town,
    string FullAddress) : IRequest<Result<string>>;