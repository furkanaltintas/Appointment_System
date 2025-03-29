using AppointmentSystemServer.Application.Features.Responses.DoctorResponses;
using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Queries.DoctorQueries;

public record GetAllDoctorQuery() : IRequest<Result<List<GetAllDoctorResponse>>>;