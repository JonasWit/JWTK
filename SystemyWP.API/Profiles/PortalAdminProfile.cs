using AutoMapper;
using SystemyWP.API.Dtos.General;
using SystemyWP.Data.Models.General;

namespace SystemyWP.API.Profiles;

public class GeneralAppProfile : Profile
{
    public GeneralAppProfile()
    {
        CreateMap<User, UserDto>();
    }
}