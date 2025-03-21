using BitCrafts.Module.Users.Abstraction.Entities;

namespace BitCrafts.Module.Users.Abstraction.UseCases;

public class UpdateUserUseCaseInput
{
    public UpdateUserUseCaseInput(User user)
    {
        User = user;
    }

    public User User { get; set; }
}