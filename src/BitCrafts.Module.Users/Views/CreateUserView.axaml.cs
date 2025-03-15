using Avalonia.Interactivity;
using BitCrafts.Infrastructure.Avalonia.Views;
using BitCrafts.Module.Users.Abstraction.Views;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Module.Users.Views;

public partial class CreateUserView : BaseControl, ICreateUserView
{
    public CreateUserView()
    {
        InitializeComponent();
    }
    public CreateUserView(ILogger<CreateUserView> logger)
        : base(logger)
    {
        InitializeComponent();
    }

    protected override void OnAppeared()
    {
    }

    protected override void OnDisappeared()
    {
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