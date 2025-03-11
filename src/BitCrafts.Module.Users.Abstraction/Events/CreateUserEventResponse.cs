using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Module.Users.Abstraction.Entities;

namespace BitCrafts.Module.Users.Abstraction.Events;

public class CreateUserEventResponse : BaseEventResponse
{
    public User User { get; set; }
}