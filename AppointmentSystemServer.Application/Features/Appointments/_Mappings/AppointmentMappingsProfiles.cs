using AppointmentSystemServer.Application.Features.Appointments.GetAll;
using AppointmentSystemServer.Domain.Entities;
using AutoMapper;

namespace AppointmentSystemServer.Application.Features.Appointments._Mappings;

public class AppointmentMappingsProfiles : Profile
{
    public AppointmentMappingsProfiles()
    {
        CreateMap<Appointment, GetAllAppointmentsQueryResponse>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Patient.FullName))
            .ReverseMap();

        #region AutoMapper yapısının gücü
        //List<GetAllAppointmentsQueryResponse> getAllAppointmentsQueryResponses = appointments
        //.Select(a => new GetAllAppointmentsQueryResponse(
        //    a.Id.ToString(),
        //    a.StartDate,
        //    a.EndDate,
        //    a.Patient.FullName,
        //    new PatientDto(
        //        a.Patient.Id,
        //        a.Patient.FirstName,
        //        a.Patient.LastName,
        //        a.Patient.IdentityNumber,
        //        a.Patient.City,
        //        a.Patient.Town,
        //        a.Patient.FullName))).ToList();
        #endregion
    }
}
