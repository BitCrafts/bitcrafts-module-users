using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Infrastructure.Abstraction.Repositories;
using BitCrafts.Infrastructure.Abstraction.Services;
using BitCrafts.Infrastructure.Abstraction.UseCases;
using BitCrafts.Module.Users.Abstraction.Entities;
using BitCrafts.Module.Users.Abstraction.Events;
using BitCrafts.Module.Users.Abstraction.Repositories;
using BitCrafts.Module.Users.Abstraction.UseCases;
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
        var userAccount = new UserAccount
        {
            HashedPassword = hashedPassword,
            PasswordSalt = salt
        };
        User user;
        user = input.User;
        user.UserAccount = userAccount;

        if (input.ResponsibleId > 0)
        {
            var family = new Family
            {
                ResponsibleId = input.ResponsibleId
            };
            user.Families.Add(family);
            user.FamilyRole = input.FamilyRole;
            await _repositoryUnitOfWork.GetRepository<IFamilyRepository>().AddAsync(family);
        }

        user = await _repositoryUnitOfWork.GetRepository<IUsersRepository>().AddAsync(user).ConfigureAwait(false);
        var result = await _repositoryUnitOfWork.CommitAsync().ConfigureAwait(false);

        _eventAggregator.Publish(new CreateUserEvent(user, result > 0));
    }
}