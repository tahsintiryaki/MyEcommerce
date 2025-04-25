using Microsoft.EntityFrameworkCore;

namespace MyEcommerce.Order.Persistence.Contexts;

public static class OrderDbMigrationExtensions
{
    public static async Task DatabaseMigrator(this OrderDbContext dbContext)
    {
        await dbContext.Database.MigrateAsync();
    }
}