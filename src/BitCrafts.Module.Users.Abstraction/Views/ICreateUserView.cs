using BitCrafts.Infrastructure.Abstraction.Application.Views;
using BitCrafts.Module.Users.Abstraction.Entities;

namespace BitCrafts.Module.Users.Abstraction.Views;

public interface ICreateUserView : IView
{
    event EventHandler CloseDialog;
    event EventHandler<User> CreateUser;
}