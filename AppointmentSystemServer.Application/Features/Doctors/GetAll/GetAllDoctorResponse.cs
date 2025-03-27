using AppointmentSystemServer.Application.Features.Departments.GetAll;

namespace AppointmentSystemServer.Application.Features.Doctors.GetAll;

public record GetAllDoctorResponse(string Id, string FirstName, string LastName, string FullName, GetAllDepartmentResponse Department);