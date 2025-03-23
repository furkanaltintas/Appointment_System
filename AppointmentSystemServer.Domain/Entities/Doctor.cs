using AppointmentSystemServer.Domain.Commons;

namespace AppointmentSystemServer.Domain.Entities;

public class Doctor : BaseEntity
{
    public int DepartmentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";

    public virtual Department Department { get; set; }

    public Doctor()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
    }

    public Doctor(int departmentId, string firstName, string lastName)
    {
        DepartmentId = departmentId;
        FirstName=firstName;
        LastName=lastName;
    }
}