using System;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using BitCrafts.Infrastructure;
using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Application.Avalonia.Extensions;
using BitCrafts.Infrastructure.Application.Avalonia.Managers;
using BitCrafts.Infrastructure.Extensions;
using BitCrafts.Module.Users.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.UsersDemoApplicationAvalonia;

public partial class App : BaseApplication
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        base.Initialize();
    }
}