using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Infrastructure.Abstraction.Repositories;
using BitCrafts.Infrastructure.Abstraction.Services;
using BitCrafts.Infrastructure.Abstraction.UseCases;
using BitCrafts.Module.Users.Abstraction.Events;
using BitCrafts.Module.Users.Abstraction.Repositories;
using BitCrafts.Module.Users.Abstraction.UseCases.UserUseCases;
using BitCrafts.Module.Users.Abstraction.UseCases.UserUseCases.Inputs;
using BitCrafts.Module.Users.Entities;

namespace BitCrafts.Module.Users.UseCases.UserUsesCases;

public sealed class CreateUserUseCase : BaseUseCase<CreateUserUseCaseInput>, ICreateUserUseCase
{
    private readonly IEventAggregator _eventAggregator;
    private readonly IHashingService _hashingService;
    private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;

    public CreateUserUseCase(IHashingService hashingService, IEventAggregator eventAggregator,
        UsersDbContext dbContext, IRepositoryUnitOfWork repositoryUnitOfWork)
    {
        _hashingService = hashingService;
        _eventAggregator = eventAggregator;
        _repositoryUnitOfWork = repositoryUnitOfWork;
        _repositoryUnitOfWork.SetDbContext(dbContext);
    }

    protected override async Task ExecuteCore(CreateUserUseCaseInput input)
    {
        var salt = _hashingService.GenerateSalt();
        var hashedPassword = _hashingService.HashPassword(input.Password);

        input.User.HashedPassword = hashedPassword;
        input.User.PasswordSalt = salt;

        var createdUser = await _repositoryUnitOfWork.GetRepository<IUsersRepository>().AddAsync(input.User)
            .ConfigureAwait(false);
        var result = await _repositoryUnitOfWork.CommitAsync().ConfigureAwait(false);

        _eventAggregator.Publish(new CreateUserEvent(createdUser, result > 0));
    }
}