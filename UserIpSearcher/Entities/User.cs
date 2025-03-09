using System.ComponentModel.DataAnnotations;

namespace UserIpSearcher.Entities;

public class User
{
    public Guid Id { get; set; }

    [Required]
    public long AccountNumber { get; set; }

    [Required]
    public List<UserIpEvent> UserIpEvents { get; set; }

    public User(long accountNumber)
    {
        AccountNumber = accountNumber;
    }
}

