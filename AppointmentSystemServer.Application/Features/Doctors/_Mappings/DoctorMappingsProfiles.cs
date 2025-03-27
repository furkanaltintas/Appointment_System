using AppointmentSystemServer.Application.Features.Doctors.Create;
using AppointmentSystemServer.Application.Features.Doctors.GetAll;
using AppointmentSystemServer.Application.Features.Doctors.Update;
using AppointmentSystemServer.Domain.Entities;
using AutoMapper;

namespace AppointmentSystemServer.Application.Features.Doctors._Mappings;

public class DoctorMappingsProfiles : Profile
{
    public DoctorMappingsProfiles()
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