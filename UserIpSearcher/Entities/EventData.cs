using System.ComponentModel.DataAnnotations;

namespace UserIpSearcher.Entities;

public class EventData
{
    public Guid Id { get; set; }

    [Required]
    public DateTime CreateDate { get; set; }

    [Required]
    public UserIpEvent UserIpEvent { get; set; }

    internal EventData(
        DateTime date)
    {
        CreateDate = date;
    }

    private EventData(Guid id)
    {
        Id = id;
    }
}