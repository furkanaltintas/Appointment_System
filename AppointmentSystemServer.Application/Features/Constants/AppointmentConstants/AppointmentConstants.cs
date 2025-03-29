namespace AppointmentSystemServer.Application.Features.Constants.AppointmentConstants;

public static class AppointmentConstants
{
    public const string CacheKey = "Appointment:GetAll";
    public static string CacheKeyGeyByDoctorId(int doctorId) => $"Appointment:GetByDoctorId:{doctorId}";


    public const string NotFound = "Appointment not found";
    public const string YouCannotDeleteACompleted = "You cannot delete a completed appointment";
    public const string DateIsNotAvailable = "Appointment date is not available";
    public const string ValidateAppointmentDate = "You cannot schedule an appointment for a past date. Please enter a valid date.";


    public const string CreateMessage = "Appointment create is successful";
    public const string UpdateMessage = "Appointment update is successful";
    public const string DeleteMessage = "Appointment delete is successful";
}