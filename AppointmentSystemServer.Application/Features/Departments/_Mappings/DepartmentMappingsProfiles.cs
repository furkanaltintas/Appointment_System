using AppointmentSystemServer.Application.Features.Departments.Create;
using AppointmentSystemServer.Application.Features.Departments.GetAll;
using AppointmentSystemServer.Application.Features.Departments.Update;
using AppointmentSystemServer.Domain.Entities;
using AutoMapper;

namespace AppointmentSystemServer.Application.Features.Departments._Mappings;

public class DepartmentMappingsProfiles : Profile
{
    public DepartmentMappingsProfiles()
    {
        CreateMap<Department, GetAllDepartmentResponse>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            .ReverseMap();

        CreateMap<Department, CreateDepartmentCommand>()
            .ReverseMap();

        CreateMap<Department, UpdateDepartmentCommand>()
            .ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

    }
}