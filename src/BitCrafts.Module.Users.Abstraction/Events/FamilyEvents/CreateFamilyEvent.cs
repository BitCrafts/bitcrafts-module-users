using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Module.Users.Abstraction.Entities;

namespace BitCrafts.Module.Users.Abstraction.Events.FamilyEvents;

public class CreateFamilyEvent : BaseEvent
{
    public CreateFamilyEvent(Family family, bool created)
    {
        Family = family;
        Created = created;
    }

    public Family Family { get; set; }
    public bool Created { get; set; }
}