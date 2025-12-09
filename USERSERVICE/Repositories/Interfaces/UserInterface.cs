using System.Data.Common;
using UserService.Model;

namespace SmartRide.Interfaces;

public interface UserInterface
{
    Task<int> RegisterUser(UserModel user);

    Task<bool> CheckUsername(string username);
    Task<bool> CheckEmailExists(string email);
    Task<(int, string)> VerifyPasswordWithEmail(string email, string password);
    Task<(int, string)> VerifyPasswordWithUsername(string username, string password);
    // Task<UserModel> Login(string identifier, string password);
}