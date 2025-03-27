using AppointmentSystemServer.Domain.Commons;

namespace AppointmentSystemServer.Domain.Entities;

public class Appointment : BaseEntity
{
    public int DoctorId { get; set; }
    public int PatientId { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsCompleted { get; set; }


    public virtual Doctor Doctor { get; set; }
    public virtual Patient Patient { get; set; }
}