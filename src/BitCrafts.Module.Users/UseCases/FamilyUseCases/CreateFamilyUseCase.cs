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

public class CreateFamilyUseCase : BaseUseCase<CreateFamilyUseCaseInput>, ICreateFamilyUseCase
{
    private readonly IEventAggregator _eventAggregator;
    private readonly IRepositoryUnitOfWork _repositoryUnitOfWork;
    private readonly ILogger<CreateFamilyUseCase> _logger;

    public CreateFamilyUseCase(IRepositoryUnitOfWork repositoryUnitOfWork,
        IEventAggregator eventAggregator,
        UsersDbContext dbContext,
        ILogger<CreateFamilyUseCase> logger)
    {
        _repositoryUnitOfWork = repositoryUnitOfWork;
        _eventAggregator = eventAggregator;
        _repositoryUnitOfWork.SetDbContext(dbContext);
        _logger = logger;
    }

    protected override async Task ExecuteCore(CreateFamilyUseCaseInput input)
    {
        try
        {
            var family = new Family
            {
                ResponsibleId = input.ResponsibleId
            };

            // Add the family to the database
            await _repositoryUnitOfWork.GetRepository<IRepository<Family>>().AddAsync(family);
            await _repositoryUnitOfWork.CommitAsync();

            // Add members to the family
            if (input.MemberIds != null)
            {
                var userRepository = _repositoryUnitOfWork.GetRepository<IUsersRepository>();
                foreach (var memberId in input.MemberIds)
                {
                    var member = await userRepository.GetByIdAsync(memberId);
                    if (member != null)
                    {
                        member.Families.Add(family);
                    }
                    else
                    {
                        _logger.LogWarning($"Member with ID {memberId} not found.");
                    }
                }

                await _repositoryUnitOfWork.CommitAsync();
            }

            _eventAggregator.Publish(new CreateFamilyEvent(family, true));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating family.");
        }
    }
}