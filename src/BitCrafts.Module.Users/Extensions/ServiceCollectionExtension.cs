using BitCrafts.Module.Users.Abstraction.Entities;
using BitCrafts.Module.Users.Abstraction.Presenters;
using BitCrafts.Module.Users.Abstraction.Repositories;
using BitCrafts.Module.Users.Abstraction.UseCases;
using BitCrafts.Module.Users.Abstraction.Views;
using BitCrafts.Module.Users.Presenters;
using BitCrafts.Module.Users.Repositories;
using BitCrafts.Module.Users.UseCases;
using BitCrafts.Module.Users.Views;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Module.Users.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddUsersModuleServices(this IServiceCollection services)
    {
        services.AddTransient<ICreateUserUseCase, CreateUserUseCase>();
        services.AddTransient<IDeleteUserUseCase, DeleteUserUseCase>();
        services.AddTransient<IUpdateUserUseCase, UpdateUserUseCase>();
        services.AddTransient<IDisplayUsersUseCase, DisplayUsersUseCase>();
        services.AddTransient<ICreateUserView, CreateUserView>();
        services.AddTransient<ICreateUserPresenter, CreateUserPresenter>();
        services.AddTransient<IUsersView, UsersView>();
        services.AddTransient<IUsersPresenter, UsersPresenter>();
        services.AddTransient<IUsersRepository, UsersRepository>();
        services.AddDbContext<UsersDbContext>();
        return services;
    }
}