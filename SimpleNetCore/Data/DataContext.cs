using Microsoft.EntityFrameworkCore;
using SimpleNetCore.Models;

namespace SimpleNetCore.Data;
public class DataContext(DbContextOptions<DataContext> options)
    : DbContext(options)
{
    public DbSet<Channel> Channels => Set<Channel>();

    public DbSet<Message> Messages => Set<Message>();
}

    