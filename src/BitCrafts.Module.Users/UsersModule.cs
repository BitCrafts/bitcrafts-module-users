using System;
using BitCrafts.Infrastructure.Abstraction.Modules;
using BitCrafts.Module.Users.Abstraction;
using BitCrafts.Module.Users.Abstraction.Presenters;
using BitCrafts.Module.Users.Abstraction.Views;
using BitCrafts.Module.Users.Presenters;
using BitCrafts.Module.Users.Views;
using BitCrafts.Module.Users.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Module.Users;

public sealed class UsersModule : IUsersModule
{
    public string Name { get; } = "Users";

    public void RegisterServices(IServiceCollection services)
    {
        services.AddUsersModuleServices();
        services.AddSingleton<IModule>(this);
    }

    public Type GetViewType()
    {
        return typeof(IUsersModuleMainView);
    }

    public Type GetViewImplementationType()
    {
        return typeof(UsersModuleMainView);
    }

    public Type GetPresenterType()
    {
        return typeof(IUsersModuleMainPresenter);
    }

    public Type GetPresenterImplementationType()
    {
        return typeof(UsersModuleMainPresenter);
    }
}