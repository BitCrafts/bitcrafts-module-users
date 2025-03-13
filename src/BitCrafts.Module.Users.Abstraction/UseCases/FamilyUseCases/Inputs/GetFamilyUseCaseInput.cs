using BitCrafts.Infrastructure.Abstraction.UseCases;

namespace BitCrafts.Module.Users.Abstraction.UseCases.FamilyUseCases.Inputs;

public sealed class GetFamilyUseCaseInput
{
    public int FamilyId { get; set; }
}