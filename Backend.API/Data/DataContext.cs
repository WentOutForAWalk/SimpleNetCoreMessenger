using Microsoft.EntityFrameworkCore;
using Backend.API.Models;

namespace Backend.API.Data;
public class DataContext(DbContextOptions<DataContext> options)
    : DbContext(options)
{
    public DbSet<Channel> Channels => Set<Channel>();

    public DbSet<Message> Messages => Set<Message>();
}

    