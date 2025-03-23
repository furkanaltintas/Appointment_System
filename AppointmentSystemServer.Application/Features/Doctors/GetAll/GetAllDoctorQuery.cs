using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Doctors.GetAll;

public sealed record GetAllDoctorQuery() : IRequest<Result<List<GetAllDoctorResponse>>>;