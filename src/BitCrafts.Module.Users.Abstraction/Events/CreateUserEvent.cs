using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Module.Users.Abstraction.Entities;

namespace BitCrafts.Module.Users.Abstraction.Events;

public class CreateUserEvent : BaseEvent
{
    public CreateUserEvent(User user, bool created)
    {
        User = user;
        Created = created;
    }

    public User User { get; set; }
    public bool Created { get; set; }
}