using AutoMapper;
using SystemyWP.API.Data.DTOs.General;
using SystemyWP.API.Data.Models;

namespace SystemyWP.API.Profiles;

public class GeneralAppProfile : Profile
{
    public GeneralAppProfile()
    {
        CreateMap<User, UserDto>();
    }
}