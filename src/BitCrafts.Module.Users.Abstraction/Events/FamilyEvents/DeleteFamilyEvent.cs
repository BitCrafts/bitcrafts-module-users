using BitCrafts.Infrastructure.Abstraction.Events;

namespace BitCrafts.Module.Users.Abstraction.Events.FamilyEvents;

public class DeleteFamilyEvent : BaseEvent
{
    public DeleteFamilyEvent(int familyId, bool deleted)
    {
        FamilyId = familyId;
        Deleted = deleted;
    }

    public int FamilyId { get; set; }
    public bool Deleted { get; set; }
}