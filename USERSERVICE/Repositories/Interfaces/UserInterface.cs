using UserService.Model;

namespace SmartRide.Interfaces;

public interface UserInterface
{
    Task<int> RegisterUser(UserModel user);
}