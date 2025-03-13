using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Application.Views;

namespace BitCrafts.Module.Users.Abstraction.Views;

public interface IUsersModuleMainView : IView
{
    void SetupPresenters(params IPresenter[] presenters);
}