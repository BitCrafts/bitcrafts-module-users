using BitCrafts.Module.Users.Abstraction.Presenters;
using BitCrafts.Module.Users.Abstraction.Presenters.User;
using BitCrafts.Module.Users.Abstraction.Repositories;
using BitCrafts.Module.Users.Abstraction.UseCases.UserUseCases;
using BitCrafts.Module.Users.Abstraction.Views;
using BitCrafts.Module.Users.Entities;
using BitCrafts.Module.Users.Presenters;
using BitCrafts.Module.Users.Presenters.User;
using BitCrafts.Module.Users.Repositories;
using BitCrafts.Module.Users.UseCases.UserUsesCases;
using BitCrafts.Module.Users.Views;
using BitCrafts.Module.Users.Views.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; 

namespace BitCrafts.Module.Users.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddUsersModuleServices(this IServiceCollection services)
    {
        services.AddDbContext<UsersDbContext>((serviceProvider, options) =>
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var defaultDatabaseConnectionString = configuration["ApplicationSettings:DefaultConnectionString"];
            if (string.IsNullOrWhiteSpace(defaultDatabaseConnectionString))
            {
                defaultDatabaseConnectionString = "InternalDb";
            }

            options.UseSqlite(configuration.GetConnectionString(defaultDatabaseConnectionString));
        });

        services.AddViews().AddPresenters().AddRepositories().AddUseCases();

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IUsersRepository, UsersRepository>(); 
        return services;
    }

    private static IServiceCollection AddPresenters(this IServiceCollection services)
    {
        services.AddTransient<ICreateUserPresenter, CreateUserPresenter>();
        services.AddTransient<IUsersPresenter, UsersPresenter>();
        services.AddTransient<IUsersModuleMainPresenter, UsersModuleMainPresenter>();
        return services;
    }

    private static IServiceCollection AddViews(this IServiceCollection services)
    {
        services.AddTransient<ICreateUserView, CreateUserView>();
        services.AddTransient<IUsersView, UsersView>();
        services.AddTransient<IUsersModuleMainView, UsersModuleMainView>();
        return services;
    }

    private static IServiceCollection AddUseCases(this IServiceCollection services)
    {
        services.AddTransient<ICreateUserUseCase, CreateUserUseCase>();
        services.AddTransient<IDeleteUserUseCase, DeleteUserUseCase>();
        services.AddTransient<IUpdateUserUseCase, UpdateUserUseCase>();
        services.AddTransient<IDisplayUsersUseCase, DisplayUsersUseCase>();
        return services;
    }
}