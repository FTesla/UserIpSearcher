using AutoMapper;
using UserIpSearcher.DbContexts;
using UserIpSearcher.ModelsDto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using UserIpSearcher.Entities;
using UserIpSearcher.Models;

namespace UserIpSearcher.EndpointHandlers;

/// <summary>
///     Request handlers for create events.
/// </summary>
public static class UserIpEventHandler
{
    public static async Task<Ok<IEnumerable<UserDto>>> CreateUserIpDataAsync(
        UserIpSearcherDbContext dbContext,
        IMapper mapper,
        ILogger<UserDto> logger)
    {

        for (var i = 0; i < 10; i++)
        {
            var newEvent = EventGenerator();
            var newEventData = new EventData(DateTime.UtcNow);

            var existingEvent = await dbContext.UserIpEvents
                .Include(e => e.User)
                .Include(e => e.Ip)
                .FirstOrDefaultAsync(e => e.User.AccountNumber == newEvent.AccountNumber && e.Ip.Address == newEvent.IpAddress);

            if (existingEvent == null)
            {
                var user = await dbContext.Users
                    .FirstOrDefaultAsync(u => u.AccountNumber == newEvent.AccountNumber);

                var ip = await dbContext.Ips
                    .FirstOrDefaultAsync(u => u.Address == newEvent.IpAddress);

                var userIpEvent = new UserIpEvent(
                    user ?? new User(newEvent.AccountNumber),
                    ip ?? new Ip(newEvent.IpAddress),
                    newEventData);
                await dbContext.UserIpEvents.AddAsync(userIpEvent);
            }
            else
            {
                existingEvent.Data.Add(newEventData);
            }

            await dbContext.SaveChangesAsync();
        }

        return TypedResults.Ok(mapper.Map<IEnumerable<UserDto>>(await dbContext.Users.ToListAsync()));
    }

    private static NewEvent EventGenerator()
    {
        var random = new Random();
        return new NewEvent()
        {
            AccountNumber = random.Next(1005, 1007),
            IpAddress = $"192.168.0.{random.Next(100, 102)}"
        };
    }
}