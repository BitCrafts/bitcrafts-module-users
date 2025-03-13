using BitCrafts.Module.Users.Abstraction.Entities;

namespace BitCrafts.Module.Users.Abstraction.UseCases.UserUseCases.Inputs;

public class UpdateUserUseCaseInput
{
    public UpdateUserUseCaseInput(User user)
    {
        User = user;
    }

    public User User { get; set; }
    public int ResponsibleId { get; set; }
    public FamilyRole FamilyRole { get; set; }
}