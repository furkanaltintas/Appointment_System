using AppointmentSystemServer.Domain.Commons;

namespace AppointmentSystemServer.Domain.Entities;

public class Patient : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public string IdentityNumber { get; set; }
    public string City { get; set; }
    public string Town { get; set; }
    public string FullAddress { get; set; }


    public Patient()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        IdentityNumber = string.Empty;
        City = string.Empty;
        Town = string.Empty;
        FullAddress = string.Empty;
    }

    public Patient(string firstName, string lastName, string identityNumber, string city, string town, string fullAddress)
    {
        FirstName = firstName;
        LastName =lastName;
        IdentityNumber = identityNumber;
        City = city;
        Town = town;
        FullAddress = fullAddress;
    }
}