using BitCrafts.Infrastructure.Abstraction.UseCases;
using BitCrafts.Module.Users.Abstraction.Entities;

namespace BitCrafts.Module.Users.Abstraction.UseCases.FamilyUseCases.Inputs;

public sealed class UpdateFamilyUseCaseInput
{
    public Family Family { get; set; }
}