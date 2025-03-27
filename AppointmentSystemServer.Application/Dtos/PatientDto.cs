namespace AppointmentSystemServer.Application.Dtos;

public class PatientDto
{
    public PatientDto()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        IdentityNumber = string.Empty;
        City = string.Empty;
        Town = string.Empty;
        FullAddress = string.Empty;
    }

    public PatientDto(int id, string firstName, string lastName, string ıdentityNumber, string city, string town, string fullAddress)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        IdentityNumber = ıdentityNumber;
        City = city;
        Town = town;
        FullAddress = fullAddress;
    }

    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public string IdentityNumber { get; set; }
    public string City { get; set; }
    public string Town { get; set; }
    public string FullAddress { get; set; }
}