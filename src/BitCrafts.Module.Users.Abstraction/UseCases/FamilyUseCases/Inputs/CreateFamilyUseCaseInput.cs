using BitCrafts.Infrastructure.Abstraction.UseCases;

namespace BitCrafts.Module.Users.Abstraction.UseCases.FamilyUseCases.Inputs;

public sealed class CreateFamilyUseCaseInput
{
    public int ResponsibleId { get; set; }
    public List<int> MemberIds { get; set; }
}