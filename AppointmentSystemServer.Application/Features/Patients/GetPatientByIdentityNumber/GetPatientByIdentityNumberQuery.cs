using AppointmentSystemServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Patients.GetPatientByIdentityNumber;

public record GetPatientByIdentityNumberQuery(string IdentityNumber) : IRequest<Result<Patient>>;