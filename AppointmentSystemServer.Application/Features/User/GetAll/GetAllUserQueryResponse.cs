namespace AppointmentSystemServer.Application.Features.User.GetAll;

public class GetAllUserQueryResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName {  get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public List<Guid> RoleIds { get; set; }
    public List<String> RoleNames { get; set; }
}