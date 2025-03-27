using AppointmentSystemServer.Domain.Commons;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentSystemServer.Domain.Entities;

public class Doctor : BaseEntity
{
    public int DepartmentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";

    public Department Department { get; set; }

    public ICollection<Appointment> Appointments { get; set; }
}