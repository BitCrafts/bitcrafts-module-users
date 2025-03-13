using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Infrastructure.Abstraction.Repositories;
using BitCrafts.Infrastructure.Abstraction.UseCases;
using BitCrafts.Module.Users.Abstraction.Events;
using BitCrafts.Module.Users.Abstraction.Repositories;
using BitCrafts.Module.Users.Abstraction.UseCases;
using BitCrafts.Module.Users.Abstraction.UseCases.UserUseCases;
using BitCrafts.Module.Users.Abstraction.UseCases.UserUseCases.Inputs;
using BitCrafts.Module.Users.Entities;

namespace BitCrafts.Module.Users.UseCases.UserUsesCases;

public sealed class UpdateUserUseCase : BaseUseCase<UpdateUserUseCaseInput>, IUpdateUserUseCase
{
    private readonly IEventAggregator _eventAggregator;
    private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;

    public UpdateUserUseCase(IRepositoryUnitOfWork repositoryUnitOfWork, UsersDbContext dbContext,
        IEventAggregator eventAggregator)
    {
        _repositoryUnitOfWork = repositoryUnitOfWork;
        _eventAggregator = eventAggregator;
        _repositoryUnitOfWork.SetDbContext(dbContext);
    }

    protected override async Task ExecuteCore(UpdateUserUseCaseInput input)
    {
        input.User.FamilyRole = input.FamilyRole;
        _repositoryUnitOfWork.GetRepository<IUsersRepository>().Update(input.User);
        var result = await _repositoryUnitOfWork.CommitAsync().ConfigureAwait(false);
        _eventAggregator.Publish(new UpdateUserEvent(input.User, result > 0));
    }
}