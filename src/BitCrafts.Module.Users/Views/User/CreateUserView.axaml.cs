using Avalonia.Interactivity;
using BitCrafts.Infrastructure.Avalonia.Views;
using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Module.Users.Abstraction.Entities;
using BitCrafts.Module.Users.Abstraction.Events;
using BitCrafts.Module.Users.Abstraction.Views;

namespace BitCrafts.Module.Users.Views.User;

public partial class CreateUserView : BaseView, ICreateUserView
{
    public CreateUserView()
    {
        InitializeComponent();
    }


    public override void UnsetBusy()
    {
        LoadingOverlay.IsVisible = false;
        base.UnsetBusy();
    }

    public override void SetBusy(string message)
    {
        LoadingOverlay.IsVisible = true;
        base.SetBusy(message);
    }
 
    private void CloseButtonOnClick(object sender, RoutedEventArgs e)
    {
        CloseDialog?.Invoke(this, EventArgs.Empty);
    }

    private void AddButtonOnClick(object sender, RoutedEventArgs e)
    {
        CreateUser?.Invoke(this, GetUser());
    }

    private Abstraction.Entities.User GetUser()
    {
        var user = new Abstraction.Entities.User
        {
            FirstName = FirstNameTextBox.Text,
            LastName = LastNameTextBox.Text,
            Email = EmailTextBox.Text,
            PhoneNumber = PhoneNumberTextBox.Text,
            BirthDate = BirthDatePicker.SelectedDate.HasValue ? BirthDatePicker.SelectedDate.Value : default,
            NationalNumber = NationalNumberTextBox.Text,
            PassportNumber = PassportNumberTextBox.Text,
            Password = PasswordTextBox.Text?.Trim()
        };
        return user;
    }

    public event EventHandler CloseDialog;
    public event EventHandler<Abstraction.Entities.User> CreateUser;
}