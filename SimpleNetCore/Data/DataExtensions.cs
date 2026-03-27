using Microsoft.EntityFrameworkCore;
using SimpleNetCore.Models;

namespace SimpleNetCore.Data;
public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<SimpleNetCoreContext>();
        dbContext.Database.Migrate();
    }


    public static void AddSimpleNetCoreDb(this WebApplicationBuilder builder)
    {
        var connString = builder.Configuration.GetConnectionString("SimpleNetCore");
        builder.Services.AddSqlite<SimpleNetCoreContext>(
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

