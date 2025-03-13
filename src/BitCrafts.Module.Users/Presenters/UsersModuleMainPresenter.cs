using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Module.Users.Abstraction.Presenters;
using BitCrafts.Module.Users.Abstraction.Views;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Module.Users.Presenters;

public sealed class UsersModuleMainPresenter : BasePresenter<IUsersModuleMainView>, IUsersModuleMainPresenter
{
    public UsersModuleMainPresenter(IUsersModuleMainView view,
        ILogger<BasePresenter<IUsersModuleMainView>> logger) :
        base(view, logger)
    {
    }

    protected override void OnInitialize()
    {
    }
}