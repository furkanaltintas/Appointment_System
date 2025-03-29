using AppointmentSystemServer.Application.Features.User.Create;
using AppointmentSystemServer.Application.Features.User.GetAll;
using AppointmentSystemServer.Application.Features.User.Update;
using AppointmentSystemServer.Domain.Entities;
using AutoMapper;

namespace AppointmentSystemServer.Application.Features.Users._Mappings;

public class UserMappingsProfiles : Profile
{
    public UserMappingsProfiles()
    {
        CreateMap<AppUser, GetAllUserQueryResponse>().ReverseMap();

        CreateMap<AppUser, CreateUserCommand>().ReverseMap();
        CreateMap<AppUser, UpdateUserCommand>().ReverseMap();
    }
}