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
    public DbSet<UserAccount> UserAccounts { get; set; }
    public DbSet<Family> Families { get; set; }
 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Family>()
            .HasMany(f => f.Members)
            .WithMany(u => u.Families);

        modelBuilder.Entity<Family>()
            .HasOne(f => f.Responsible)
            .WithMany()
            .HasForeignKey(f => f.ResponsibleId);
    }
}