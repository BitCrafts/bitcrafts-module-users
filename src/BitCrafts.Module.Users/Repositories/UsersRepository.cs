using BitCrafts.Infrastructure.Abstraction.Repositories;
using BitCrafts.Module.Users.Abstraction.Entities;
using BitCrafts.Module.Users.Abstraction.Repositories;

namespace BitCrafts.Module.Users.Repositories;

public class UsersRepository : Repository<UsersDbContext, User>, IUsersRepository
{
    public UsersRepository(UsersDbContext context) : base(context)
    {
    }
}