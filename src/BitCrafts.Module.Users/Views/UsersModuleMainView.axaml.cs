using Avalonia.Controls;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Avalonia.Views;
using BitCrafts.Module.Users.Abstraction.Views;

namespace BitCrafts.Module.Users.Views;

public partial class UsersModuleMainView : BaseView, IUsersModuleMainView
{
    private IList<IPresenter> _presenters;

    public UsersModuleMainView()
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
                Content = presenter.GetView().GetTitle()
            });
        }
    }
}