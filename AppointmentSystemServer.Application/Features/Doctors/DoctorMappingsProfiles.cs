using AppointmentSystemServer.Application.Features.Doctors.Create;
using AppointmentSystemServer.Application.Features.Doctors.GetAll;
using AppointmentSystemServer.Application.Features.Doctors.Update;
using AppointmentSystemServer.Domain.Entities;
using AutoMapper;

namespace AppointmentSystemServer.Application.Features.Doctors;

public class DoctorMappingsProfiles : Profile
{
    public DoctorMappingsProfiles()
    {
        CreateMap<Doctor, GetAllDoctorResponse>()
            .ForMember(opt => opt.DepartmentName, dest => dest.MapFrom(src => src.Department.Name))
            .ReverseMap();

        CreateMap<Doctor, CreateDoctorCommand>()
            .ReverseMap();

        CreateMap<Doctor, UpdateDoctorCommand>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
    }
}