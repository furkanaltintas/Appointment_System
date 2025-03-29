using AppointmentSystemServer.Application.Features.Responses.AppointmentResponses;
using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Queries.AppointmentQueries;

public record GetAllAppointmentsQuery(int DoctorId) : IRequest<Result<List<GetAllAppointmentsQueryResponse>>>;