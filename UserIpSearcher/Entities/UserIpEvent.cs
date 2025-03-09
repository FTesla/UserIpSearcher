using System.ComponentModel.DataAnnotations;

namespace UserIpSearcher.Entities;

public class UserIpEvent
{
    public Guid Id { get; set; }

    [Required]
    public Guid IpId { get; set; }

    [Required]
    public Guid UserId { get; set; }

    public Ip Ip { get; set; }

    public User User { get; set; }

    [Required]
    public List<EventData> Data { get; set; } = new List<EventData>();

    internal UserIpEvent(User user, Ip ip, EventData date)
    {
        User = user;
        Ip = ip;
        UserId = user.Id;
        IpId = ip.Id;
        Data.Add(date);
    }

    public UserIpEvent()
    {
    }
}