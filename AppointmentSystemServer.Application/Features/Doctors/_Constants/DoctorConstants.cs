namespace AppointmentSystemServer.Application.Features.Doctors._Constants;

public static class DoctorConstants
{
    public const string CacheKey = "Doctor:GetAll"; // tüm doktorları listeleme
    public static string CacheKeyWrite(int departmentId) => $"Doctor:Write:DepartmentId:{departmentId}"; // Ekleme yaparken
    public static string CacheKeyDepartmentById(int departmentId) => $"Doctor:GetByDepartmentById:{departmentId}"; //  Departmana göre doktor listeleme




    public const string NoSuch = "There is no such doctor in the system.";
    public const string NotFound = "Doctor not found.";


    public const string CreateMessage = "Doctor create is successful";
    public const string DeleteMessage = "Doctor delete is successful";
    public const string UpdateMessage = "Doctor update is successful";
}
