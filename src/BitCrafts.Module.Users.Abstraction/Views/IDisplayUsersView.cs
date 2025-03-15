using BitCrafts.Infrastructure.Abstraction.Application.Views;
using BitCrafts.Module.Users.Abstraction.Entities;

namespace BitCrafts.Module.Users.Abstraction.Views;

public interface IDisplayUsersView : IView
{
    void RefreshUsers(IEnumerable<User> users);
}