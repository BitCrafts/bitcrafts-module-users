using System;
using BitCrafts.Infrastructure.Abstraction.Application.Views;
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
    public Type GetPresenterType()
    {
        return typeof(IUsersModuleMainPresenter);
    }

    public string Name { get; } = "Users";

    public void RegisterServices(IServiceCollection services)
    {
        services.AddUsersModuleServices();
        services.AddSingleton<IModule>(this);
        services.AddSingleton<IUsersModule>(this);
    }

    public Dictionary<string, Type> GetViews()
    {
        Dictionary<string, Type> views = new Dictionary<string, Type>();
        views.Add("Users", typeof(IUsersView));
        return views;
    }
}