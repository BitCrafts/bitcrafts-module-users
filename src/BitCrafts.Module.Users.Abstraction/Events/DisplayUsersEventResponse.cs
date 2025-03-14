using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Module.Users.Abstraction.Entities;

namespace BitCrafts.Module.Users.Abstraction.Events;

public class DisplayUsersEventResponse : BaseEventResponse
{
    public DisplayUsersEventResponse(IEnumerable<User> users)
    {
        Users = users;
    }

    public IEnumerable<User> Users { get; private set; }
}