using AutoMapper;
using UserIpSearcher.Entities;
using UserIpSearcher.Models;
using UserIpSearcher.ModelsDto;

namespace UserIpSearcher.Profiles;

/// <summary>
///     Mapping profile.
/// </summary>
public class SearchProfile : Profile
{
    public SearchProfile()
    {
        CreateMap<UserIpEvent, UserDto>();
        CreateMap<Ip, IpDto>();
        CreateMap<LastUserConnect, LastUserConnectDto>();
    }
}
