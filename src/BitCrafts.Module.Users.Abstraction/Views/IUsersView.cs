using BitCrafts.Infrastructure.Abstraction.Application.Views;
using BitCrafts.Module.Users.Abstraction.Entities;

namespace BitCrafts.Module.Users.Abstraction.Views;

public interface IUsersView : IView
{
    event EventHandler<User> UserUpdated;
    event EventHandler<User> UserDeleted;
    
    event EventHandler OpenCreateUserDialog;
    void RefreshUsers(IEnumerable<User> users);
    void DeleteUser(int userId);
    void AppendUser(User user);
}