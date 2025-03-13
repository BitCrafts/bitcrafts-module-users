using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Module.Users.Abstraction.Presenters;
using BitCrafts.Module.Users.Abstraction.Presenters.User;
using BitCrafts.Module.Users.Abstraction.Views;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Module.Users.Presenters;

public sealed class UsersModuleMainPresenter : BasePresenter<IUsersModuleMainView>, IUsersModuleMainPresenter
{
    private readonly IUsersPresenter _usersPresenter;

    public UsersModuleMainPresenter(IUsersModuleMainView view,IUsersPresenter usersPresenter,
        ILogger<BasePresenter<IUsersModuleMainView>> logger) :
        base(view, logger)
    {
        _usersPresenter = usersPresenter;
    }

    protected override void OnInitialize()
    {
        View.AppendPresenter(_usersPresenter);
    }
}