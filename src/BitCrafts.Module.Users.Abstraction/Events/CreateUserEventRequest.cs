using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Module.Users.Abstraction.Entities;

namespace BitCrafts.Module.Users.Abstraction.Events;

public class CreateUserEventRequest : BaseEventRequest
{
    public User User { get; set; }
    public string Password { get; set; }
}