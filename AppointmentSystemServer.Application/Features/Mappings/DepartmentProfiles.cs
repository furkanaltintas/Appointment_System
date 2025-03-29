using AppointmentSystemServer.Application.Features.Commands.DepartmentCommands;
using AppointmentSystemServer.Application.Features.Responses.DepartmentResponses;
using AppointmentSystemServer.Domain.Entities;
using AutoMapper;

namespace AppointmentSystemServer.Application.Features.Mappings;

public class DepartmentProfiles : Profile
{
    public DepartmentProfiles()
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