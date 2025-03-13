using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Module.Users.Abstraction.Entities;

namespace BitCrafts.Module.Users.Abstraction.Events.FamilyEvents;

public class GetFamilyEvent : BaseEvent
{
    public Family Family { get; set; }
}