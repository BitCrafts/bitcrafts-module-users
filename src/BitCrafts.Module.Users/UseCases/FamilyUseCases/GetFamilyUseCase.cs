using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Infrastructure.Abstraction.Repositories;
using BitCrafts.Infrastructure.Abstraction.UseCases;
using BitCrafts.Module.Users.Abstraction.Entities;
using BitCrafts.Module.Users.Abstraction.Events.FamilyEvents;
using BitCrafts.Module.Users.Abstraction.UseCases.FamilyUseCases;
using BitCrafts.Module.Users.Abstraction.UseCases.FamilyUseCases.Inputs;
using BitCrafts.Module.Users.Entities;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Module.Users.UseCases.FamilyUseCases;

public class GetFamilyUseCase : BaseUseCase<GetFamilyUseCaseInput>, IGetFamilyUseCase
{
    private readonly IEventAggregator _eventAggregator;
    private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;
    private readonly ILogger<GetFamilyUseCase> _logger;

    public GetFamilyUseCase(IRepositoryUnitOfWork repositoryUnitOfWork,
        IEventAggregator eventAggregator,
        UsersDbContext dbContext,
        ILogger<GetFamilyUseCase> logger)
    {
        _repositoryUnitOfWork = repositoryUnitOfWork;
        _eventAggregator = eventAggregator;
        _repositoryUnitOfWork.SetDbContext(dbContext);
        _logger = logger;
    }

    protected override async Task ExecuteCore(GetFamilyUseCaseInput input)
    {
        try
        {
            var family = await _repositoryUnitOfWork.GetRepository<IRepository<Family>>().GetByIdAsync(input.FamilyId);
            if (family == null)
            {
                _logger.LogWarning($"Family with ID {input.FamilyId} not found.");
            }

            _eventAggregator.Publish(new GetFamilyEvent()
            {
                Family = family
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting family with ID {familyId}.", input.FamilyId);
        }
    }
}