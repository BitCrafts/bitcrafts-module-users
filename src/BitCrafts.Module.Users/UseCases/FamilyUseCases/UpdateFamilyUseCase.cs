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

public sealed class UpdateFamilyUseCase : BaseUseCase<UpdateFamilyUseCaseInput>, IUpdateFamilyUseCase
{
    private readonly IEventAggregator _eventAggregator;
    private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;
    private readonly ILogger<UpdateFamilyUseCase> _logger;

    public UpdateFamilyUseCase(IRepositoryUnitOfWork repositoryUnitOfWork,
        IEventAggregator eventAggregator,
        UsersDbContext dbContext,
        ILogger<UpdateFamilyUseCase> logger)
    {
        _repositoryUnitOfWork = repositoryUnitOfWork;
        _eventAggregator = eventAggregator;
        _repositoryUnitOfWork.SetDbContext(dbContext);
        _logger = logger;
    }

    protected override async Task ExecuteCore(UpdateFamilyUseCaseInput input)
    {
        try
        {
            _repositoryUnitOfWork.GetRepository<IRepository<Family>>().Update(input.Family);
            var result = await _repositoryUnitOfWork.CommitAsync();

            _eventAggregator.Publish(new UpdateFamilyEvent(input.Family, result > 0));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating family with ID {familyId}.", input.Family.Id);
        }
    }
}