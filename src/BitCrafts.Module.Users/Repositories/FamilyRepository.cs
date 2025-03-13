using BitCrafts.Infrastructure.Abstraction.Repositories;
using BitCrafts.Module.Users.Abstraction.Entities;
using BitCrafts.Module.Users.Abstraction.Repositories;
using BitCrafts.Module.Users.Entities;

namespace BitCrafts.Module.Users.Repositories;

public sealed class FamilyRepository : Repository<UsersDbContext, Family>, IFamilyRepository
{
    public FamilyRepository(UsersDbContext context) : base(context)
    {
    }
}