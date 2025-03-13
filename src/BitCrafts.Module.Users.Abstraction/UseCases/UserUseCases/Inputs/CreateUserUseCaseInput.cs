using BitCrafts.Module.Users.Abstraction.Entities;

namespace BitCrafts.Module.Users.Abstraction.UseCases.UserUseCases.Inputs;

public class CreateUserUseCaseInput
{
    public string Password { get; set; }
    public User User { get; set; }
}