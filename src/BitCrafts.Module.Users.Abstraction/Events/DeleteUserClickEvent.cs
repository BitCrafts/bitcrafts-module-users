using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Module.Users.Abstraction.Entities;

namespace BitCrafts.Module.Users.Abstraction.Events;

public class DeleteUserClickEvent : BaseEvent
{
    public DeleteUserClickEvent(User user)
    {
        User = user;
    }

    public User User { get; set; }
}