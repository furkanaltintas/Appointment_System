using AppointmentSystemServer.Domain.Commons;
using System.ComponentModel.DataAnnotations.Schema;

namespace AppointmentSystemServer.Domain.Entities;

public class Patient : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";
    public string IdentityNumber { get; set; }
    public string City { get; set; }
    public string Town { get; set; }
    public string FullAddress { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new HashSet<Appointment>();
}