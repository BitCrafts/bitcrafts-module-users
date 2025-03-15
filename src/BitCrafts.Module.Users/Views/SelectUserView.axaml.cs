using BitCrafts.Infrastructure.Avalonia.Views;
using BitCrafts.Module.Users.Abstraction.Views;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Module.Users.Views;

public partial class SelectUserView : BaseControl, ISelectUserView
{
    public SelectUserView()
    {
        InitializeComponent();
    }
    public SelectUserView(ILogger<SelectUserView> logger) : base(logger)
    {
        InitializeComponent();
    }

    protected override void OnAppeared()
    {
    }

    protected override void OnDisappeared()
    {
    }
}