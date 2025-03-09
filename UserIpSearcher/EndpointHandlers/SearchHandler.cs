using AutoMapper;
using UserIpSearcher.DbContexts;
using UserIpSearcher.ModelsDto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using UserIpSearcher.Models;

namespace UserIpSearcher.EndpointHandlers;

/// <summary>
///     Request handlers for find user and ip.
/// </summary>
public static class SearchHandler
{
    public static async Task<Ok<IEnumerable<UserDto>>> GetUsersByIpAsync(
        UserIpSearcherDbContext dbContext,
        IMapper mapper,
        ILogger<UserDto> logger,
        [FromQuery] string ipAddressSearch)
    {
        var users = dbContext.Ips
            .Include(ip => ip.UserIpEvents)
            .ThenInclude(e => e.User)
            .Where(ip => ip.Address.Contains(ipAddressSearch))
            .SelectMany(ip => ip.UserIpEvents.Select(e => e.User))
            .Distinct();

        return TypedResults.Ok(mapper.Map<IEnumerable<UserDto>>(users));
    }

    public static async Task<Ok<IEnumerable<IpDto>>> GetIpsByUserAsync(
        UserIpSearcherDbContext dbContext,
        IMapper mapper,
        ILogger<IpDto> logger,
        [FromQuery] long accountNumber)
    {
        var users = dbContext.Users
            .Include(user => user.UserIpEvents)
            .ThenInclude(e => e.Ip)
            .Where(ip => ip.AccountNumber == accountNumber)
            .SelectMany(ip => ip.UserIpEvents.Select(e => e.Ip))
            .Distinct();

        return TypedResults.Ok(mapper.Map<IEnumerable<IpDto>>(users));
    }

    public static async Task<Ok<LastUserConnectDto>> GetLastDateConnectByUserAsync(
        UserIpSearcherDbContext dbContext,
        IMapper mapper,
        ILogger<LastUserConnectDto> logger,
        [FromQuery] long accountNumber)
    {

        var ipConnects = dbContext.Users
            .Include(user => user.UserIpEvents)
            .ThenInclude(e => e.Ip)
            .Include(user => user.UserIpEvents)
            .ThenInclude(e => e.Data)
            .Where(u => u.AccountNumber == accountNumber)
            .SelectMany(u => u.UserIpEvents.Select(e => new
            {
                Date = e.Data.Select(d => d.CreateDate).Max(),
                Ip = e.Ip
            }));

        var lastDate = ipConnects.Max(r => r.Date);

        var ipLastConnects = ipConnects
            .Where(c => c.Date == lastDate)
            .Select(c => c.Ip.Address)
            .ToList();

        var lastUserConnect = new LastUserConnect()
        {
            Date = lastDate,
            Address = ipLastConnects
        };

        return TypedResults.Ok(mapper.Map<LastUserConnectDto>(lastUserConnect));
    }
}
