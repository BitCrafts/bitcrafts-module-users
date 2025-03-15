using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Module.Users.Abstraction.Entities;

namespace BitCrafts.Module.Users.Abstraction.Events.UserEvents;

public class DisplayUsersEvent : BaseEvent
{
    public DisplayUsersEvent(IEnumerable<User> users)
    {
        Users = users;
    }

    public IEnumerable<User> Users { get; private set; }
}