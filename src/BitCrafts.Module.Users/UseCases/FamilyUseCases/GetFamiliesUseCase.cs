using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Infrastructure.Abstraction.Repositories;
using BitCrafts.Infrastructure.Abstraction.UseCases;
using BitCrafts.Module.Users.Abstraction.Entities;
using BitCrafts.Module.Users.Abstraction.Events.FamilyEvents;
using BitCrafts.Module.Users.Abstraction.Repositories;
using BitCrafts.Module.Users.Abstraction.UseCases.FamilyUseCases;
using BitCrafts.Module.Users.Abstraction.UseCases.FamilyUseCases.Inputs;
using BitCrafts.Module.Users.Entities;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Module.Users.UseCases.FamilyUseCases;

public sealed class GetFamiliesUseCase : BaseUseCase<GetFamiliesUseCaseInput>, IGetFamiliesUseCase
{
    private readonly IEventAggregator _eventAggregator;
    private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;
    private readonly ILogger<GetFamiliesUseCase> _logger;

    public GetFamiliesUseCase(IRepositoryUnitOfWork repositoryUnitOfWork,
        IEventAggregator eventAggregator,
        UsersDbContext dbContext,
        ILogger<GetFamiliesUseCase> logger)
    {
        _repositoryUnitOfWork = repositoryUnitOfWork;
        _eventAggregator = eventAggregator;
        _repositoryUnitOfWork.SetDbContext(dbContext);
        _logger = logger;
    }

    protected override async Task ExecuteCore(GetFamiliesUseCaseInput input)
    {
        try
        {
            List<Family> families = null;
            if (input.UserId > 0)
            {
                var user = await _repositoryUnitOfWork.GetRepository<IUsersRepository>().GetByIdAsync(input.UserId);
                if (user != null)
                {
                    families = user.Families.ToList();
                }
                else
                {
                    _logger.LogWarning($"User with ID {input.UserId} not found.");
                }
            }
            else
            {
                families = (await _repositoryUnitOfWork.GetRepository<IFamilyRepository>().GetAllAsync())?.ToList();
            }

            if (families != null && families.Any())
            {
                _eventAggregator.Publish(new GetFamiliesEvent()
                {
                    Families = families
                });
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting families for user with ID {userId}.", input.UserId);
        }
    }
}