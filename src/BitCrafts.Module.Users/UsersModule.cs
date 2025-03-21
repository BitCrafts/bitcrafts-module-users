using BitCrafts.Infrastructure.Abstraction.Modules;
using BitCrafts.Module.Users.Abstraction;
using BitCrafts.Module.Users.Abstraction.Entities;
using BitCrafts.Module.Users.Abstraction.Presenters;
using BitCrafts.Module.Users.Abstraction.Repositories;
using BitCrafts.Module.Users.Abstraction.UseCases;
using BitCrafts.Module.Users.Abstraction.Views;
using BitCrafts.Module.Users.Presenters;
using BitCrafts.Module.Users.Views;
using BitCrafts.Module.Users.Extensions;
using BitCrafts.Module.Users.Repositories;
using BitCrafts.Module.Users.UseCases;
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
        return typeof(IUsersView);
    }

    public Type GetViewImplementationType()
    {
        return typeof(UsersView);
    }

    public Type GetPresenterType()
    {
        return typeof(IUsersPresenter);
    }

    public Type GetPresenterImplementationType()
    {
        return typeof(UsersPresenter);
    }
}