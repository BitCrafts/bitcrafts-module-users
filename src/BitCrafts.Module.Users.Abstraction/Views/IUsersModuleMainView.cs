using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Application.Views;
using BitCrafts.Module.Users.Abstraction.Presenters;

namespace BitCrafts.Module.Users.Abstraction.Views;

public interface IUsersModuleMainView : IView
{
    void AppendPresenter(IPresenter presenter);
}