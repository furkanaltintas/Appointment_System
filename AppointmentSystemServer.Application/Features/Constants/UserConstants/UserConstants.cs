namespace AppointmentSystemServer.Application.Features.Constants.UserConstants;

public static class UserConstants
{
    public const string CacheKey = "User:GetAll";

    public const string NotFound = "User not found";
    public const string EmailAlreadyExists = "UserName already exists";
    public const string UserNameAlreadyExists = "UserName already exists";


    public const string CreateMessage = "User create is successful";
    public const string UpdateMessage = "User update is successful";
    public const string DeleteMessage = "User delete is successful";
}