using SmartRide.Interfaces;
using UserService.Model;

namespace SmartRide.Implementations;

public class UserRepo : UserInterface
{
    private string _connectionString;
    public UserRepo(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<int> RegisterUser(UserModel user)
    {

        return 0;
    }
}