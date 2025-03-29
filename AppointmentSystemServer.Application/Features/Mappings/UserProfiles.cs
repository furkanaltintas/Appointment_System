using AppointmentSystemServer.Application.Features.Commands.UserCommands;
using AppointmentSystemServer.Application.Features.Responses.UserResponses;
using AppointmentSystemServer.Domain.Entities;
using AutoMapper;

namespace AppointmentSystemServer.Application.Features.Mappings;

public class UserProfiles : Profile
{
    public UserProfiles()
    {
        CreateMap<AppUser, GetAllUserQueryResponse>().ReverseMap();

        CreateMap<AppUser, CreateUserCommand>().ReverseMap();
        CreateMap<AppUser, UpdateUserCommand>().ReverseMap();
    }
}