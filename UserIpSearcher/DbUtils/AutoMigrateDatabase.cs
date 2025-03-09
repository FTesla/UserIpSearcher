using Microsoft.EntityFrameworkCore;

namespace UserIpSearcher.DbUtils;

/// <summary>
///     Automatic migration utility.
/// </summary>
public static class AutoMigrateDatabase
{
    /// <summary>
    ///     Automatically apply migrations to a database.
    /// </summary>
    public static async Task MigrateDatabase(DbContext context)
    {
        var migrations = await context.Database.GetPendingMigrationsAsync();
        if (migrations.Any())
        {
            Console.WriteLine($"Pending migrations...");

            foreach (var migration in migrations)
            {
                Console.WriteLine($"[+] pending migration: {migration}");
            }

            await context.Database.MigrateAsync();

            Console.WriteLine($"Migrations have been applied!");
        }
    }
}

