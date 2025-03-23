namespace AppointmentSystemServer.Application.Features.Doctors.GetAll;

public sealed record GetAllDoctorResponse(string DepartmentName, string FirstName, string LastName, string FullName);