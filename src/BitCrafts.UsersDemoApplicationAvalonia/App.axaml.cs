using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Application.Avalonia.Extensions;
using BitCrafts.Infrastructure.Application.Avalonia.Managers;
using BitCrafts.Infrastructure.Extensions;
using BitCrafts.Module.Users.Abstraction.Presenters;
using BitCrafts.Module.Users.Abstraction.Views;
using BitCrafts.Module.Users.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.UsersDemoApplicationAvalonia;

public partial class App : global::Avalonia.Application
{
    private IServiceProvider _serviceProvider;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var services = new ServiceCollection();
        services.AddBitCraftsInfrastructure().AddBitCraftsAvaloniaApplication();
        services.AddUsersModuleServices();
        _serviceProvider = services.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var uiManager = (AvaloniaUiManager)_serviceProvider.GetRequiredService<IUiManager>();
            desktop.ShutdownMode = ShutdownMode.OnLastWindowClose;
            uiManager.SetNativeApplication(desktop);
        }

        base.OnFrameworkInitializationCompleted();
    }
}