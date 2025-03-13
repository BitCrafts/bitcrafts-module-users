using BitCrafts.Module.Users.Abstraction.Entities;
using Microsoft.EntityFrameworkCore;

namespace BitCrafts.Module.Users.Entities;

public sealed class UsersDbContext : DbContext
{
    public UsersDbContext(DbContextOptions<UsersDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}