using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Module.Users.Abstraction.Entities;

namespace BitCrafts.Module.Users.Abstraction.Events.FamilyEvents;

public class UpdateFamilyEvent : BaseEvent
{
    public UpdateFamilyEvent(Family family, bool updated)
    {
        Family = family;
        Updated = updated;
    }

    public Family Family { get; private set; }
    public bool Updated { get; set; }
}