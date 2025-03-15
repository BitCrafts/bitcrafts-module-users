using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Module.Users.Abstraction.Events.UserEvents;
using BitCrafts.Module.Users.Abstraction.Presenters;
using BitCrafts.Module.Users.Abstraction.UseCases;
using BitCrafts.Module.Users.Abstraction.UseCases.Inputs;
using BitCrafts.Module.Users.Abstraction.Views;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Module.Users.Presenters;

public sealed class UsersPresenter : BasePresenter<IUsersView>, IUsersPresenter
{
    private readonly IDeleteUserUseCase _deleteUserUseCase;
    private readonly IDisplayUsersUseCase _displayUsersUseCase;
    private readonly IEventAggregator _eventAggregator;
    private readonly IUpdateUserUseCase _updateUserUseCase;
    private readonly IWindowManager _windowManager;

    public UsersPresenter(IDisplayUsersUseCase displayUsersUseCase,
        IUpdateUserUseCase updateUserUseCase, IDeleteUserUseCase deleteUserUseCase,
        IWindowManager windowManager, IUsersView view, ILogger<UsersPresenter> logger,
        IEventAggregator eventAggregator) : base(view, logger)
    {
        _windowManager = windowManager;
        _eventAggregator = eventAggregator;
        _displayUsersUseCase = displayUsersUseCase;
        _updateUserUseCase = updateUserUseCase;
        _deleteUserUseCase = deleteUserUseCase;
        View.Title = "Users";
    }


    private void OnDeleteUser(DeleteUserEvent obj)
    {
        View.DeleteUser(obj.UserId);
    }

    private void OnDisplayUsers(DisplayUsersEvent obj)
    {
        View.RefreshUsers(obj.Users);
    }

    private async void ViewOnOpenCreateUserDialog(object sender, EventArgs args)
    {
        await _windowManager.ShowPresenterAsync<ICreateUserPresenter>();
    }

    private void OnUserCreated(CreateUserEvent obj)
    {
        if (obj.Created)
            View.AppendUser(obj.User);
    }

    private async void OnUpdateUserClicked(object sender, Abstraction.Entities.User obj)
    {
        await _updateUserUseCase.Execute(new UpdateUserUseCaseInput(obj));
    }

    private async void OnDeleteUserClicked(object sender, Abstraction.Entities.User obj)
    {
        await _deleteUserUseCase.Execute(new DeleteUserUseCaseInput(obj));
    }

    protected override async Task OnAppearedAsync()
    {
        View.UserDeleted += OnDeleteUserClicked;
        View.UserUpdated += OnUpdateUserClicked;
        View.OpenCreateUserDialog += ViewOnOpenCreateUserDialog;
        _eventAggregator.Subscribe<CreateUserEvent>(OnUserCreated);
        _eventAggregator.Subscribe<DisplayUsersEvent>(OnDisplayUsers);
        _eventAggregator.Subscribe<DeleteUserEvent>(OnDeleteUser);
        View.SetBusy("Loading users...");
        await _displayUsersUseCase.Execute();
        View.UnsetBusy();
    }

    protected override Task OnDisAppearedAsync()
    {
        View.UserDeleted -= OnDeleteUserClicked;
        View.UserUpdated -= OnUpdateUserClicked;
        _eventAggregator.Unsubscribe<CreateUserEvent>(OnUserCreated);
        _eventAggregator.Unsubscribe<DisplayUsersEvent>(OnDisplayUsers);
        return Task.CompletedTask;
    }
}