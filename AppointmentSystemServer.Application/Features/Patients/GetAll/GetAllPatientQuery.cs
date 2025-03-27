using AppointmentSystemServer.Domain.Entities;
using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Patients.GetAll;

public record GetAllPatientQuery() : IRequest<Result<List<Patient>>>;