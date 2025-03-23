using AppointmentSystemServer.Domain.Commons;
using Microsoft.AspNetCore.Identity;

namespace AppointmentSystemServer.Domain.Entities;

public class AppUser : IdentityUser<Guid>, IEntity
{
    // sealed => Bu classın başka bir class tarafından inherit edilmesini engelliyor

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";

    public AppUser()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
    }

    public AppUser(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}