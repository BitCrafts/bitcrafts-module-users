using Avalonia.Controls;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Avalonia.Views;
using BitCrafts.Module.Users.Abstraction.Views;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Module.Users.Views;

public partial class UsersModuleMainView : BaseControl, IUsersModuleMainView
{
    private IList<IPresenter> _presenters;

    public UsersModuleMainView()
    {
        InitializeComponent();
    }

    public UsersModuleMainView(ILogger<UsersModuleMainView> logger)
        : base(logger)
    {
        InitializeComponent();
    }

    private void FeaturesListVox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (FeaturesListVox.SelectedIndex >= 0 && FeaturesListVox.SelectedIndex < _presenters.Count)
        {
            CurrentContent.Content = _presenters[FeaturesListVox.SelectedIndex].GetView();
        }
    }

    public void SetupPresenters(params IPresenter[] presenters)
    {
        _presenters = presenters.ToList();
        foreach (var presenter in _presenters)
        {
            FeaturesListVox.Items.Add(new ListBoxItem()
            {
                Content = presenter.GetView().Title
            });
        }
    }

    protected override void OnAppeared()
    {
    }

    protected override void OnDisappeared()
    {
    }
}