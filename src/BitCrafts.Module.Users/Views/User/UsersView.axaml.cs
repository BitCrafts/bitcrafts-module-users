using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using BitCrafts.Infrastructure.Avalonia.Views;
using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Module.Users.Abstraction.Events;
using BitCrafts.Module.Users.Abstraction.Views;

namespace BitCrafts.Module.Users.Views.User;

public partial class UsersView : BaseView, IUsersView
{
    private readonly IEventAggregator _eventAggregator;
    private readonly ObservableCollection<Abstraction.Entities.User> _users;


    public UsersView()
    {
        InitializeComponent();
    }

    public UsersView(IEventAggregator eventAggregator) : this()
    {
        _users = new ObservableCollection<Abstraction.Entities.User>();

        _eventAggregator = eventAggregator;

        UsersDataGrid.ItemsSource = _users;
    }

    public void RefreshUsers(IEnumerable<Abstraction.Entities.User> users)
    {
        _users.Clear();
        foreach (var user in users) _users.Add(user);

        UsersDataGrid.ItemsSource = _users;
    }

    public override void SetBusy(string message)
    {
        LoadingOverlay.IsVisible = true;
        base.SetBusy(message);
    }

    public override void UnsetBusy()
    {
        LoadingOverlay.IsVisible = false;
        base.UnsetBusy();
    }

    protected override void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (Design.IsDesignMode) return;
        _eventAggregator.Subscribe<CreateUserEvent>(OnCreateUser);
        _eventAggregator.Subscribe<DeleteUserEvent>(OnDeleteUser);
        _eventAggregator.Subscribe<DisplayUsersEvent>(OnDisplayUsers);
        base.OnLoaded(sender, e);
    }

    private void OnDisplayUsers(DisplayUsersEvent obj)
    {
        RefreshUsers(obj.Users);
    }

    private void OnDeleteUser(DeleteUserEvent obj)
    {
        var user = _users.FirstOrDefault(u => u.Id == obj.UserId);
        if (user != null) _users.Remove(user);
    }

    protected override void OnUnloaded(object sender, RoutedEventArgs e)
    {
        base.OnUnloaded(sender, e);
        if (Design.IsDesignMode) return;
        _eventAggregator.Unsubscribe<CreateUserEvent>(OnCreateUser);
        _eventAggregator.Unsubscribe<DeleteUserEvent>(OnDeleteUser);
        _eventAggregator.Unsubscribe<DisplayUsersEvent>(OnDisplayUsers);
    }

    private void OnCreateUser(CreateUserEvent obj)
    {
        AppendUser(obj.User);
    }

    public void AppendUser(Abstraction.Entities.User user)
    {
        _users.Add(user);
        UsersDataGrid.ItemsSource = _users;
    }

    private void SaveButton_OnClick(object sender, RoutedEventArgs e)
    {
        _eventAggregator.Publish(new AddUserClickEvent());
    }

    private void Closebutton_OnClick(object sender, RoutedEventArgs e)
    {
        _eventAggregator.Publish(new UsersPresenterCloseEvent());
    }

    private void UsersDataGrid_OnRowEditEnded(object sender, DataGridRowEditEndedEventArgs e)
    {
        if (e.EditAction == DataGridEditAction.Commit)
        {
            var user = e.Row.DataContext as Abstraction.Entities.User;
            if (user != null) _eventAggregator.Publish(new UpdateUserClickEvent(user));
        }
    }

    private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
    {
        if (UsersDataGrid.SelectedItem is Abstraction.Entities.User user) _eventAggregator.Publish(new DeleteUserClickEvent(user));
    }
}