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

public sealed class DeleteFamilyUseCase : BaseUseCase<DeleteFamilyUseCaseInput>,IDeleteFamilyUseCase
{
    private readonly IEventAggregator _eventAggregator;
    private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;
    private readonly ILogger<DeleteFamilyUseCase> _logger;

    public DeleteFamilyUseCase(IRepositoryUnitOfWork repositoryUnitOfWork,
        IEventAggregator eventAggregator,
        UsersDbContext dbContext,
        ILogger<DeleteFamilyUseCase> logger)
    {
        _repositoryUnitOfWork = repositoryUnitOfWork;
        _eventAggregator = eventAggregator;
        _repositoryUnitOfWork.SetDbContext(dbContext);
        _logger = logger;
    }
    
    protected override async Task ExecuteCore(DeleteFamilyUseCaseInput input)
    {
        try
        {
            var family = await _repositoryUnitOfWork.GetRepository<IRepository<Family>>().GetByIdAsync(input.FamilyId);
            if (family != null)
            {
                _repositoryUnitOfWork.GetRepository<IRepository<Family>>().Remove(family);
                await _repositoryUnitOfWork.CommitAsync();

                _eventAggregator.Publish(new DeleteFamilyEvent(input.FamilyId, true)); // We'll create this event later
            }
            else
            {
                _logger.LogWarning($"Family with ID {input.FamilyId} not found.");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting family with ID {familyId}.", input.FamilyId); 
        }
    }
}