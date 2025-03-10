using BitCrafts.Infrastructure.Abstraction.Application.Views;
using BitCrafts.Users.Abstraction.Entities;

namespace BitCrafts.Users.Abstraction.Views;

public interface IUsersView : IView
{
    void RefreshUsers(IEnumerable<User> users);
}