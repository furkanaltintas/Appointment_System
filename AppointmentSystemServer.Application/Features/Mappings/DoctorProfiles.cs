using AppointmentSystemServer.Application.Features.Commands.DoctorCommands;
using AppointmentSystemServer.Application.Features.Responses.DoctorResponses;
using AppointmentSystemServer.Domain.Entities;
using AutoMapper;

namespace AppointmentSystemServer.Application.Features.Mappings;

public class DoctorProfiles : Profile
{
    public DoctorProfiles()
    {
        CreateMap<Doctor, GetAllDoctorResponse>()
            .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            .ReverseMap();


        CreateMap<Doctor, CreateDoctorCommand>()
            .ReverseMap();


        CreateMap<Doctor, UpdateDoctorCommand>()
            .ReverseMap()
            .ForMember(dest => dest.FullName, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}