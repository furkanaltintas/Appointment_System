using AppointmentSystemServer.Application.Features.Departments.Create;
using AppointmentSystemServer.Application.Features.Departments.Delete;
using AppointmentSystemServer.Application.Features.Departments.GetAll;
using AppointmentSystemServer.Application.Features.Departments.Update;
using AppointmentSystemServer.Domain.Entities;
using AutoMapper;

namespace AppointmentSystemServer.Application.Features.Departments;

public class DepartmentMappingsProfiles : Profile
{
    public DepartmentMappingsProfiles()
    {
        CreateMap<Department, GetAllDepartmentResponse>().ReverseMap();
        CreateMap<Department, CreateDepartmentCommand>().ReverseMap();
        CreateMap<Department, UpdateDepartmentCommand>().ReverseMap();
        CreateMap<Department, DeleteDepartmentCommand>().ReverseMap();
    }
}