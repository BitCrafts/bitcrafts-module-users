using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Threading;
using BitCrafts.Module.Users.Abstraction.Presenters;
using BitCrafts.Module.Users.Abstraction.Views;
using BitCrafts.Module.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Module.Users.Presenters;

public sealed class UsersModuleMainPresenter : BasePresenter<IUsersModuleMainView>, IUsersModuleMainPresenter
{
    private readonly IUsersPresenter _usersPresenter;
    private readonly IDisplayUsersPresenter _displayUsersPresenter;
    private readonly IBackgroundThreadDispatcher _backgroundThreadDispatcher;
    private readonly UsersDbContext _usersDbContext;

    public UsersModuleMainPresenter(IUsersModuleMainView view, IUsersPresenter usersPresenter,
        IDisplayUsersPresenter displayUsersPresenter,
        ILogger<BasePresenter<IUsersModuleMainView>> logger, IBackgroundThreadDispatcher backgroundThreadDispatcher,
        UsersDbContext usersDbContext) :
        base(view, logger)
    {
        _usersPresenter = usersPresenter;
        _displayUsersPresenter = displayUsersPresenter;
        _backgroundThreadDispatcher = backgroundThreadDispatcher;
        _usersDbContext = usersDbContext;
        View.Title = "Users Modules";
    }

    private void ApplyDatabaseMigration()
    {
        Logger.LogInformation("Applying database migrations...");
        _usersDbContext.Database.Migrate();
    }

    protected override async Task OnAppearedAsync()
    {
        View.SetupPresenters(_displayUsersPresenter);
        View.SetBusy("Applying database migrations...");
        await _backgroundThreadDispatcher.InvokeAsync(ApplyDatabaseMigration);
    }

    protected override Task OnDisAppearedAsync()
    {
        return Task.CompletedTask;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _usersPresenter.Dispose();
        }

        base.Dispose(disposing);
    }
}