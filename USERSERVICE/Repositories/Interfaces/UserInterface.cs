using UserService.Model;

namespace SmartRide.Interfaces;

public interface UserInterface
{
    Task<int> RegisterUser(UserModel user);

    Task<bool> CheckUsername(string username);
    Task<bool> CheckEmailExists(string email);
    // Task<UserModel> Login(string identifier, string password);
}