using BitCrafts.Module.Users.Abstraction.Entities;

namespace BitCrafts.Module.Users.Abstraction.UseCases;

public class DeleteUserUseCaseInput
{
    public DeleteUserUseCaseInput(User user)
    {
        User = user;
    }

    public User User { get; set; }
}