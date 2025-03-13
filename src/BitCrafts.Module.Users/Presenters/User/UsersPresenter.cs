using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Infrastructure.Abstraction.Threading;
using BitCrafts.Module.Users.Abstraction.Events;
using BitCrafts.Module.Users.Abstraction.Presenters;
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
    private readonly IWorkspaceManager _workspaceManager;
    private readonly IBackgroundThreadDispatcher _backgroundThreadDispatcher;
    private readonly UsersDbContext _usersDbContext;

    public UsersPresenter(IDisplayUsersUseCase displayUsersUseCase,
        IUpdateUserUseCase updateUserUseCase, IDeleteUserUseCase deleteUserUseCase,
        IWindowManager windowManager, IUsersView view, ILogger<UsersPresenter> logger,
        IWorkspaceManager workspaceManager, IBackgroundThreadDispatcher backgroundThreadDispatcher,
        UsersDbContext usersDbContext,
        IEventAggregator eventAggregator) : base(view, logger)
    {
        _windowManager = windowManager;
        _workspaceManager = workspaceManager;
        _backgroundThreadDispatcher = backgroundThreadDispatcher;
        _usersDbContext = usersDbContext;
        _eventAggregator = eventAggregator;
        _displayUsersUseCase = displayUsersUseCase;
        _updateUserUseCase = updateUserUseCase;
        _deleteUserUseCase = deleteUserUseCase;
    }

    protected override async void OnViewLoaded(object sender, EventArgs e)
    {
        base.OnViewLoaded(sender, e);
        View.SetBusy("Applying database migrations...");
        await _backgroundThreadDispatcher.InvokeAsync(ApplyDatabaseMigration);
        View.SetBusy("Loading users...");
        await _displayUsersUseCase.Execute();
        View.UnsetBusy();
    }

    
    private void ApplyDatabaseMigration()
    {
        Logger.LogInformation("Applying database migrations...");
        _usersDbContext.Database.Migrate();
        Logger.LogInformation("Applied database migrations");
    }

    protected override void OnInitialize()
    {
        _eventAggregator.Subscribe<AddUserClickEvent>(OnAddUserClicked);
        _eventAggregator.Subscribe<DeleteUserClickEvent>(OnDeleteUserClicked);
        _eventAggregator.Subscribe<UpdateUserClickEvent>(OnUpdateUserClicked);
        _eventAggregator.Subscribe<UsersPresenterCloseEvent>(OnUsersPresenterClosed);
    }

    private void OnUsersPresenterClosed(UsersPresenterCloseEvent obj)
    {
        _workspaceManager.ClosePresenter<UsersPresenter>();
    }

    private async void OnUpdateUserClicked(UpdateUserClickEvent obj)
    {
        await _updateUserUseCase.Execute(new UpdateUserUseCaseInput(obj.User));
    }

    private async void OnDeleteUserClicked(DeleteUserClickEvent obj)
    {
        await _deleteUserUseCase.Execute(new DeleteUserUseCaseInput(obj.User));
    }

    private async void OnAddUserClicked(AddUserClickEvent obj)
    {
        await _windowManager.ShowDialogWindowAsync<ICreateUserPresenter>();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _eventAggregator.Unsubscribe<AddUserClickEvent>(OnAddUserClicked);
            _eventAggregator.Unsubscribe<DeleteUserClickEvent>(OnDeleteUserClicked);
            _eventAggregator.Unsubscribe<UpdateUserClickEvent>(OnUpdateUserClicked);
            _eventAggregator.Unsubscribe<UsersPresenterCloseEvent>(OnUsersPresenterClosed);
        }

        base.Dispose(disposing);
    }
}