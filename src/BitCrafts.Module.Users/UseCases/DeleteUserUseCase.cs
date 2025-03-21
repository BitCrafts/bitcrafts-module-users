using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Infrastructure.Abstraction.Repositories;
using BitCrafts.Infrastructure.Abstraction.UseCases;
using BitCrafts.Module.Users.Abstraction.Entities;
using BitCrafts.Module.Users.Abstraction.Events;
using BitCrafts.Module.Users.Abstraction.Repositories;
using BitCrafts.Module.Users.Abstraction.UseCases;

namespace BitCrafts.Module.Users.UseCases;

public sealed class DeleteUserUseCase : BaseUseCase<DeleteUserUseCaseInput>, IDeleteUserUseCase
{
    private readonly IEventAggregator _eventAggregator;
    private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;

    public DeleteUserUseCase(IRepositoryUnitOfWork repositoryUnitOfWork, UsersDbContext dbContext
        , IEventAggregator eventAggregator)
    {
        _repositoryUnitOfWork = repositoryUnitOfWork;
        _eventAggregator = eventAggregator;
        _repositoryUnitOfWork.SetDbContext(dbContext);
    }

    protected override async Task ExecuteCore(DeleteUserUseCaseInput input)
    {
        _repositoryUnitOfWork.GetRepository<IUsersRepository>().Remove(input.User);
        var result = await _repositoryUnitOfWork.CommitAsync().ConfigureAwait(false);
        _eventAggregator.Publish(new DeleteUserEvent(input.User.Id, result > 0));
    }
}