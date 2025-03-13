using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BitCrafts.Module.Users.Entities;

public class UsersDbContextFactory : IDesignTimeDbContextFactory<UsersDbContext>
{
    public UsersDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<UsersDbContext>();
        var connectionString = configuration.GetConnectionString("InternalDb");
        builder.UseSqlite(connectionString);

        return new UsersDbContext(builder.Options);
    }
}