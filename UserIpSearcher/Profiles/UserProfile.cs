using AutoMapper;
using UserIpSearcher.Entities;
using UserIpSearcher.ModelsDto;

namespace UserIpSearcher.Profiles;

/// <summary>
///     Mapping profile.
/// </summary>
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
    }
}
