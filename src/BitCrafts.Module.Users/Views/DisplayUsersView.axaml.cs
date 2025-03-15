using System.Collections.ObjectModel;
using Avalonia.Controls;
using BitCrafts.Infrastructure.Avalonia.Views;
using BitCrafts.Module.Users.Abstraction.Entities;
using BitCrafts.Module.Users.Abstraction.Views;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Module.Users.Views;

public partial class DisplayUsersView : BaseControl, IDisplayUsersView
{
    private ObservableCollection<Abstraction.Entities.User> _users = new();
    private List<User> _selectedUsers = new List<User>();

    public List<User> SelectedUsers
    {
        get => _selectedUsers;
        set => _selectedUsers = value;
    }

    public DisplayUsersView()
    {
        InitializeComponent();
    }
    public DisplayUsersView(ILogger<DisplayUsersView> logger) : base(logger)
    {
        InitializeComponent();
    }

    private void UsersDataGridOnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        SelectedUsers.Clear();
        SelectedUsers.AddRange(e.AddedItems.Cast<User>().ToArray());
        e.Handled = true;
    }

    public void RefreshUsers(IEnumerable<Abstraction.Entities.User> users)
    {
        _users.Clear();
        foreach (var user in users)
        {
            _users.Add(user);
        }
    }

    protected override void OnAppeared()
    {
        UsersDataGrid.ItemsSource = _users;
        UsersDataGrid.SelectionChanged += UsersDataGridOnSelectionChanged;
    }

    protected override void OnDisappeared()
    {
        UsersDataGrid.SelectionChanged -= UsersDataGridOnSelectionChanged;
    }
}