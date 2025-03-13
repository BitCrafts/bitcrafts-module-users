using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Module.Users.Abstraction.Events;
using BitCrafts.Module.Users.Abstraction.Presenters.User;
using BitCrafts.Module.Users.Abstraction.UseCases.UserUseCases;
using BitCrafts.Module.Users.Abstraction.UseCases.UserUseCases.Inputs;
using BitCrafts.Module.Users.Abstraction.Views;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Module.Users.Presenters.User;

public class CreateUserPresenter : BasePresenter<ICreateUserView>, ICreateUserPresenter
{
    private readonly ICreateUserUseCase _createUserUseCase;
    private readonly IEventAggregator _eventAggregator;
    private readonly IWindowManager _windowManager;

    public CreateUserPresenter(ICreateUserView view, ICreateUserUseCase createUserUseCase,
        IEventAggregator eventAggregator, IWindowManager windowManager,
        ILogger<CreateUserPresenter> logger) : base(view,
        logger)
    {
        _createUserUseCase = createUserUseCase;
        _eventAggregator = eventAggregator;
        _windowManager = windowManager;
        
    }

    protected override void OnViewLoaded(object sender, EventArgs e)
    {
        View.SetTitle("Create User");
        base.OnViewLoaded(sender, e);
    }

    protected override void OnInitialize()
    {
        View.CloseDialog += ViewOnCloseDialog;
        View.CreateUser += ViewOnCreateUser;
    }

    private async void ViewOnCreateUser(object sender, Abstraction.Entities.User e)
    {
        var useCaseInput = new CreateUserUseCaseInput
        {
            User = e,
            Password = e.Password
        };
        View.SetBusy("Loading...");
        await _createUserUseCase.Execute(useCaseInput);
        View.UnsetBusy();
    }

    private void ViewOnCloseDialog(object sender, EventArgs e)
    {
        _windowManager.CloseWindow<ICreateUserPresenter>();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            View.CloseDialog -= ViewOnCloseDialog;
            View.CreateUser -= ViewOnCreateUser;
        }

        base.Dispose(disposing);
    }
}