using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Infrastructure.Abstraction.Threading;
using BitCrafts.Module.Users.Abstraction.Events.UserEvents;
using BitCrafts.Module.Users.Abstraction.Presenters;
using BitCrafts.Module.Users.Abstraction.UseCases;
using BitCrafts.Module.Users.Abstraction.Views;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Module.Users.Presenters;

public class DisplayUsersPresenter : BasePresenter<IDisplayUsersView>, IDisplayUsersPresenter
{
    private readonly IDisplayUsersUseCase _displayUsersUseCase;
    private readonly IBackgroundThreadDispatcher _backgroundThreadDispatcher;
    private readonly IEventAggregator _eventAggregator;
    private readonly ICreateUserUseCase _createUserUseCase;

    public DisplayUsersPresenter(IDisplayUsersView view, IDisplayUsersUseCase displayUsersUseCase,
        IBackgroundThreadDispatcher backgroundThreadDispatcher, IEventAggregator eventAggregator,
        ICreateUserUseCase createUserUseCase,
        ILogger<BasePresenter<IDisplayUsersView>> logger)
        : base(view,
            logger)
    {
        _displayUsersUseCase = displayUsersUseCase;
        _backgroundThreadDispatcher = backgroundThreadDispatcher;
        _eventAggregator = eventAggregator;
        _createUserUseCase = createUserUseCase;
    }

    private void OnUserDisplay(DisplayUsersEvent obj)
    {
        View.RefreshUsers(obj.Users);
    }

    protected override async Task OnAppearedAsync()
    {
        _eventAggregator.Subscribe<DisplayUsersEvent>(OnUserDisplay);
        await _displayUsersUseCase.Execute();
    }

    protected override Task OnDisAppearedAsync()
    {
        _eventAggregator.Unsubscribe<DisplayUsersEvent>(OnUserDisplay);
        return Task.CompletedTask;
    }
}