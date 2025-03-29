using AppointmentSystemServer.Application.Features.Responses.DepartmentResponses;

namespace AppointmentSystemServer.Application.Features.Responses.DoctorResponses;

public record GetAllDoctorResponse(string Id, string FirstName, string LastName, string FullName, GetAllDepartmentResponse Department);