using MediatR;
using TS.Result;

namespace AppointmentSystemServer.Application.Features.Appointments.CreateAppointment;

public record CreateAppointmentCommand(
    string StartDate,
    string EndDate,
    int DoctorId,
    int PatientId,
    string FirstName,
    string LastName,
    string IdentityNumber,
    string City,
    string Town,
    string FullAddress) : IRequest<Result<string>>;
