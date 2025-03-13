using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Infrastructure.Abstraction.Threading;
using BitCrafts.Module.Users.Abstraction.Events;
using BitCrafts.Module.Users.Abstraction.Presenters.User;
using BitCrafts.Module.Users.Abstraction.UseCases.UserUseCases;
using BitCrafts.Module.Users.Abstraction.UseCases.UserUseCases.Inputs;
using BitCrafts.Module.Users.Abstraction.Views;
using BitCrafts.Module.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Module.Users.Presenters.User;

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
        View.SetTitle("Users");
    }

    protected override async void OnViewLoaded(object sender, EventArgs e)
    {
        base.OnViewLoaded(sender, e);
        View.SetBusy("Loading users...");
        await _displayUsersUseCase.Execute();
        View.UnsetBusy();
    }


    protected override void OnInitialize()
    {
        View.UserDeleted += OnDeleteUserClicked;
        View.UserUpdated += OnUpdateUserClicked;
        View.OpenCreateUserDialog += ViewOnOpenCreateUserDialog;
        _eventAggregator.Subscribe<CreateUserEvent>(OnUserCreated);
        _eventAggregator.Subscribe<DisplayUsersEvent>(OnDisplayUsers);
        _eventAggregator.Subscribe<DeleteUserEvent>(OnDeleteUser);
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
        await _windowManager.ShowDialogWindowAsync<ICreateUserPresenter>();
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

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            View.UserDeleted -= OnDeleteUserClicked;
            View.UserUpdated -= OnUpdateUserClicked;
            _eventAggregator.Unsubscribe<CreateUserEvent>(OnUserCreated);
            _eventAggregator.Unsubscribe<DisplayUsersEvent>(OnDisplayUsers);
        }

        base.Dispose(disposing);
    }
}