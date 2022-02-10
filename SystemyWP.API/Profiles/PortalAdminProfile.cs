using AutoMapper;
using SystemyWP.API.Data.Models.UsersManagement;
using SystemyWP.API.DTOs;

namespace SystemyWP.API.Profiles;

public class GeneralAppProfile : Profile
{
    public GeneralAppProfile()
    {
        CreateMap<User, UserDto>();
    }
}