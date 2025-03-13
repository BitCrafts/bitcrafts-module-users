using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Module.Users.Abstraction.Entities;

namespace BitCrafts.Module.Users.Abstraction.Events;

public class CreateUserEventResponse : BaseEvent
{
    public User User { get; set; }
}