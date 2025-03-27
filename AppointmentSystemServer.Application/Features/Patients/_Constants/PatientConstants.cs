namespace AppointmentSystemServer.Application.Features.Patients._Constants;

public static class PatientConstants
{
    public const string CacheKey = "Patient:GetAll";
    public static string CacheKeyGetByIdentityNumber(string identityNumber) => $"Patient:GetByIdentityNumber:{identityNumber}";


    public const string NoSuch = "There is no such patient in the system.";
    public const string NotFound = "Patient not found.";
    public const string IdentityNumberAlreadyUse = "This identity number already use";


    public const string CreateMessage = "Patient create is successful";
    public const string UpdateMessage = "Patient update is successful";
    public const string DeleteMessage = "Patient delete is successful";
}