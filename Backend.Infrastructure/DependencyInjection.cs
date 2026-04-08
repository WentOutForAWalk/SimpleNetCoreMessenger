using Backend.Infrastructure.Data;
using Backend.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connString = configuration.GetConnectionString("Backend.API");

        services.AddDbContext<DataContext>(options =>
            options.UseSqlite(connString, sqliteOptions =>
                sqliteOptions.MigrationsAssembly("Backend.Infrastructure")));

        // Services
        services.AddScoped<ChannelService>();
        services.AddScoped<MessageService>();
        services.AddScoped<AuthService>();
        services.AddScoped<UserContextService>();

        // adds work UserContextService
        services.AddHttpContextAccessor();

        // services.AddScoped<IMessageService, MessageService>();
        services.AddIdentityApiEndpoints<IdentityUser>()
            .AddEntityFrameworkStores<DataContext>();

        return services;
    }
}

