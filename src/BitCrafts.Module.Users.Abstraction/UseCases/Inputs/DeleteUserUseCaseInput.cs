using BitCrafts.Module.Users.Abstraction.Entities;

namespace BitCrafts.Module.Users.Abstraction.UseCases.Inputs;

public class DeleteUserUseCaseInput
{
    public DeleteUserUseCaseInput(User user)
    {
        User = user;
    }

    public User User { get; set; }
}