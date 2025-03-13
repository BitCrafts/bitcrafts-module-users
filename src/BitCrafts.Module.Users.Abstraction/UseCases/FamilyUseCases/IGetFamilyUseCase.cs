using BitCrafts.Infrastructure.Abstraction.UseCases;
using BitCrafts.Module.Users.Abstraction.UseCases.FamilyUseCases.Inputs;

namespace BitCrafts.Module.Users.Abstraction.UseCases.FamilyUseCases;

public interface IGetFamilyUseCase : IUseCase<GetFamilyUseCaseInput> // Input: Family ID, Output: Family
{
}