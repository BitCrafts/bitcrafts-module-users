using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Module.Users.Abstraction.Entities;

namespace BitCrafts.Module.Users.Abstraction.Events;

public class UpdateUserClickEvent : BaseEvent
{
    public UpdateUserClickEvent(User user)
    {
        User = user;
    }

    public User User { get; set; }
}