using AppointmentSystemServer.Domain.Commons;

namespace AppointmentSystemServer.Domain.Entities;

public class Department : BaseEntity
{
    public string Name { get; set; }

    public ICollection<Doctor> Doctors { get; set; }
}