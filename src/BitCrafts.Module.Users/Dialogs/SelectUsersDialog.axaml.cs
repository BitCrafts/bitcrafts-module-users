using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BitCrafts.Module.Users.Abstraction.Dialogs;

namespace BitCrafts.Module.Users.Dialogs;

public partial class SelectUsersDialog : Window, ISelectUsersDialog
{
    public SelectUsersDialog()
    {
        InitializeComponent();
    }
}