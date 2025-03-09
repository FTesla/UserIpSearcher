using Microsoft.EntityFrameworkCore;
using UserIpSearcher.Entities;

namespace UserIpSearcher.DbContexts;

/// <summary>
///     The database context for the UserIpSearcher application.
/// </summary>
public class UserIpSearcherDbContext : DbContext
{
    public DbSet<UserIpEvent> UserIpEvents { get; set; }

    public DbSet<EventData> EventData { get; set; }

    public DbSet<Ip> Ips { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Ip alternative key setting
        modelBuilder.Entity<Ip>()
            .HasKey(i => i.Id);
        modelBuilder.Entity<Ip>()
            .HasAlternateKey(i => i.Address);

        // User alternative key setting
        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);
        modelBuilder.Entity<User>()
            .HasAlternateKey(u => u.AccountNumber);

        // UserIpEvent alternative keys setting
        modelBuilder.Entity<UserIpEvent>()
            .HasKey(u => u.Id);
        modelBuilder.Entity<UserIpEvent>()
            .HasAlternateKey(u => new {u.IpId, u.UserId});

        // UserIpEvent keys mapping
        modelBuilder.Entity<UserIpEvent>()
            .HasOne(u => u.Ip)
            .WithMany(u => u.UserIpEvents)
            .HasForeignKey(u => u.IpId);

        modelBuilder.Entity<UserIpEvent>()
            .HasOne(u => u.User)
            .WithMany(u => u.UserIpEvents)
            .HasForeignKey(u => u.UserId);
    }

    public UserIpSearcherDbContext(DbContextOptions<UserIpSearcherDbContext> options)
        : base(options)
    {
    }
}
