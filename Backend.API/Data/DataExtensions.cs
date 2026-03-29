using Microsoft.EntityFrameworkCore;
using Backend.API.Models;

namespace Backend.API.Data;
public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
        dbContext.Database.Migrate();
    }


    public static void BackendDb(this WebApplicationBuilder builder)
    {
        var connString = builder.Configuration.GetConnectionString("Backend.API");
        builder.Services.AddSqlite<DataContext>(
            connString,
            optionsAction: options => options.UseSeeding((context, _) =>
            {
                if (!context.Set<Channel>().Any())
                {
                    context.Set<Channel>().Add(new Channel()
                    {
                        ChannelName = "FirstTest"
                    });
                    context.SaveChanges();
                }
            })
        );
    }
}

