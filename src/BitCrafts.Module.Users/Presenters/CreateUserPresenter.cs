using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Module.Users.Abstraction.Presenters;
using BitCrafts.Module.Users.Abstraction.UseCases;
using BitCrafts.Module.Users.Abstraction.UseCases.Inputs;
using BitCrafts.Module.Users.Abstraction.Views;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Module.Users.Presenters;

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
        View.Title = "Create User";
    }


    private async void ViewOnCreateUser(object sender, Abstraction.Entities.User e)
    {
        var useCaseInput = new CreateUserUseCaseInput
        {
            User = e,
        };
        View.SetBusy("Loading...");
        await _createUserUseCase.Execute(useCaseInput);
        View.UnsetBusy();
    }


    protected override Task OnAppearedAsync()
    {
        View.CloseDialog += ViewOnCloseDialog;
        View.CreateUser += ViewOnCreateUser;
        return Task.CompletedTask;
    }

    private void ViewOnCloseDialog(object sender, EventArgs e)
    {
        _windowManager.ClosePresenter<ICreateUserPresenter>();
    }

    protected override Task OnDisAppearedAsync()
    {
        View.CloseDialog -= ViewOnCloseDialog;
        View.CreateUser -= ViewOnCreateUser;
        return Task.CompletedTask;
    }
}