using AppointmentSystemServer.Application.Features.Patients.Create;
using AppointmentSystemServer.Application.Features.Patients.Update;
using AppointmentSystemServer.Domain.Entities;
using AutoMapper;

namespace AppointmentSystemServer.Application.Features.Patients._Mappings;

public class PatientMappingsProfiles : Profile
{
    public PatientMappingsProfiles()
    {
        CreateMap<Patient, CreatePatientCommand>()
            .ReverseMap();

        CreateMap<Patient, UpdatePatientCommand>()
            .ReverseMap()
            .ForMember(dest => dest.FullName, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}