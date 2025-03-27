using AppointmentSystemServer.Application.Dtos;

namespace AppointmentSystemServer.Application.Features.Appointments.GetAllAppointments;

public record GetAllAppointmentsQueryResponse(
    String Id,
    DateTime StartDate,
    DateTime EndDate,
    string Title,
    PatientDto Patient);