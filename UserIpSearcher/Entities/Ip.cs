using System.ComponentModel.DataAnnotations;

namespace UserIpSearcher.Entities;

public class Ip
{
    public Guid Id { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    public List<UserIpEvent> UserIpEvents { get; set; }

    public Ip(string address)
    {
        Address = address;
    }
}