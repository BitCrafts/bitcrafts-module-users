using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BitCrafts.Infrastructure.Abstraction.Avalonia.Views;
using BitCrafts.Module.Users.Abstraction.Views;

namespace BitCrafts.Module.Users.Views;

public partial class UsersModuleMainView : BaseView, IUsersModuleMainView
{
    public UsersModuleMainView()
    {
        InitializeComponent();
    }
}