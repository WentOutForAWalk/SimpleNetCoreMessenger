using Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Extensions;
public static class MigrationExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
        dbContext.Database.Migrate();
    }
}

