using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
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

    private async Task<bool> CheckEmail(string email)
    {
        try
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();

                var query = "SELECT 1 FROM users WHERE email=@email";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        catch (System.Exception ex)
        {

            System.Console.WriteLine("Error: " + ex.Message);
            return false;
        }
    }

    public async Task<int> RegisterUser(UserModel user)
    {
        try
        {
            if (await CheckEmail(user.Email ?? ""))
            {
                return -1;
            }
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "INSERT INTO Users (username, email, mobile, password_hash, role, bio, profile_image_url, cover_image_url) VALUES (@username,@email,@mobile,@pass,@role,@bio,@profile,@cover)";
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password ?? "");
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", user.UserName ?? "");
                    cmd.Parameters.AddWithValue("@email", user.Email ?? "");
                    cmd.Parameters.AddWithValue("@mobile", user.Number ?? "");
                    cmd.Parameters.AddWithValue("@pass", hashedPassword);
                    cmd.Parameters.AddWithValue("@role", user.Role ?? "");
                    cmd.Parameters.AddWithValue("@bio", user.Bio ?? "");
                    cmd.Parameters.AddWithValue("@profile", user.ProfileImageUrl ?? "");
                    cmd.Parameters.AddWithValue("@cover", user.CoverImageUrl ?? "");

                    var result = cmd.ExecuteNonQuery();

                    if (result == 1)
                    {
                        return 1;
                    }

                    return 0;
                }
            }
        }
        catch (System.Exception ex)
        {

            System.Console.WriteLine("Erorr: " + ex.Message);
            return -500;

        }
    }

    public async Task<bool> CheckUsername(string username)
    {
        try
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "SELECT 1 FROM users WHERE username=@username LIMIT 1";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    var reader = await cmd.ExecuteScalarAsync();
                    return reader != null;
                }
            }
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Error: " + ex.Message);
            return false;
        }
    }

    public async Task<bool> CheckEmailExists(string email)
    {
        try
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "SELECT 1 FROM users WHERE email=@email LIMIT 1";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    var reader = await cmd.ExecuteScalarAsync();
                    return reader != null;
                }
            }
        }
        catch (System.Exception ex)
        {

            System.Console.WriteLine("Error: " + ex.Message);
            return false;

        }
    }

    public async Task<(int, string)> VerifyPasswordWithEmail(string email, string password)
    {
        try
        {
            string dbPassword = "";
            int id = 0;
            string role = "";
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "SELECT user_id,role, password_hash from users WHERE email=@email LIMIT 1";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@email", email);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            id = reader.GetInt32(0);
                            role = reader.GetString(1);
                            dbPassword = reader.GetString(2);
                            bool isValid = BCrypt.Net.BCrypt.Verify(password, dbPassword);
                            if (isValid)
                            {
                                return (id, role);
                            }
                        }
                    }
                }
            }
            return (id, role);
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Error: " + ex.Message);
            return (-1, "");
        }
    }
    public async Task<(int, string)> VerifyPasswordWithUsername(string username, string password)
    {
        try
        {
            string dbPassword = "";
            int id = 0;
            string role = "";
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var query = "SELECT user_id,role, password_hash from users WHERE username=@username LIMIT 1";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            id = reader.GetInt32(0);
                            role = reader.GetString(1);
                            dbPassword = reader.GetString(2);
                            bool isValid = BCrypt.Net.BCrypt.Verify(password, dbPassword);
                            if (isValid)
                            {
                                return (id, role);
                            }
                        }
                    }
                }
            }
            return (id, role);
        }
        catch (System.Exception ex)
        {
            System.Console.WriteLine("Error: " + ex.Message);
            return (-1, "");
        }
    }
}