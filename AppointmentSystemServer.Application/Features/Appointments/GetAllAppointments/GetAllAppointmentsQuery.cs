using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Appointments.GetAllAppointments;

public record GetAllAppointmentsQuery(int DoctorId) : IRequest<Result<List<GetAllAppointmentsQueryResponse>>>;