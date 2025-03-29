using AppointmentSystemServer.Application.Dtos;

namespace AppointmentSystemServer.Application.Features.Appointments.GetAll;

public class GetAllAppointmentsQueryResponse
{
   public String Id { get; set; }
   public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Title { get; set; }
    public PatientDto Patient { get; set; }
}