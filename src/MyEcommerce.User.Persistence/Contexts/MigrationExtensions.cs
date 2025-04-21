using Microsoft.EntityFrameworkCore;

namespace MyEcommerce.User.Persistence.Contexts;

public static class MigrationExtensions
{
    public static async Task DatabaseMigrator(this UserDbContext dbContext)
    {
        await dbContext.Database.MigrateAsync();
    }
}