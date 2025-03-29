using AppointmentSystemServer.Application.Dtos;
using AppointmentSystemServer.Application.Features.Commands.PatientCommands;
using AppointmentSystemServer.Domain.Entities;
using AutoMapper;

namespace AppointmentSystemServer.Application.Features.Mappings;

public class PatientProfiles : Profile
{
    public PatientProfiles()
    {
        CreateMap<Patient, CreatePatientCommand>()
            .ReverseMap();

        CreateMap<Patient, UpdatePatientCommand>()
            .ReverseMap()
            .ForMember(dest => dest.FullName, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Patient, PatientDto>()
            .ReverseMap();
    }
}