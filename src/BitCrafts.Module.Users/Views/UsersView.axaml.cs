using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using BitCrafts.Infrastructure.Avalonia.Views;
using BitCrafts.Module.Users.Abstraction.Views;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Module.Users.Views;

public partial class UsersView : BaseControl, IUsersView
{
    private readonly ObservableCollection<Abstraction.Entities.User> _users;
    public event EventHandler<Abstraction.Entities.User> UserUpdated;
    public event EventHandler<Abstraction.Entities.User> UserDeleted;
    public event EventHandler OpenCreateUserDialog;

    public UsersView()
    {
        InitializeComponent();
    }

    public UsersView(ILogger<UsersView> logger)
        : base(logger)
    {
        InitializeComponent();
        _users = new ObservableCollection<Abstraction.Entities.User>();
        UsersDataGrid.ItemsSource = _users;
    }


    protected override void OnAppeared()
    {
    }

    protected override void OnDisappeared()
    {
    }

    public void RefreshUsers(IEnumerable<Abstraction.Entities.User> users)
    {
        _users.Clear();
        foreach (var user in users) _users.Add(user);

        UsersDataGrid.ItemsSource = _users;
    }

    public void DeleteUser(int userId)
    {
        var userFound = _users.FirstOrDefault(u => u.Id == userId);
        if (userFound != null) _users.Remove(userFound);
    }


    public void AppendUser(Abstraction.Entities.User user)
    {
        _users.Add(user);
        UsersDataGrid.ItemsSource = _users;
    }

    private void SaveButton_OnClick(object sender, RoutedEventArgs e)
    {
        OpenCreateUserDialog?.Invoke(this, EventArgs.Empty);
    }

    private void UsersDataGrid_OnRowEditEnded(object sender, DataGridRowEditEndedEventArgs e)
    {
        if (e.EditAction == DataGridEditAction.Commit)
        {
            var user = e.Row.DataContext as Abstraction.Entities.User;
            if (user != null)
            {
                UserUpdated?.Invoke(this, user);
            }
        }
    }

    private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (UsersDataGrid.SelectedItem is Abstraction.Entities.User user)
        {
            UserDeleted?.Invoke(this, user);
        }
    }
}