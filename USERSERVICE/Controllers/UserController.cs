using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartRide.Interfaces;
using UserService.Model;

namespace SmartRide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserInterface _userRepo;
        public UserController(UserInterface userRepo)
        {
            _userRepo = userRepo;
        }



        [HttpPost("register-user")]
        public async Task<IActionResult> RegisterUser(UserModel user)
        {
            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                return BadRequest(new { success = false, message = "Username is required" });
            }

            user.UserName = user.UserName?.Trim().ToLower();

            if (user.UserName?.Length < 3)
            {
                return BadRequest(new { message = "Username must be at least 3 characters long", success = false });

            }

            bool userNameExist = await _userRepo.CheckUsername(user.UserName ?? "");

            if (userNameExist)
            {
                return Conflict(new { message = "Username already exist", success = false });
            }

            bool emailExist = await _userRepo.CheckEmailExists(user.Email ?? "");

            if (emailExist)
            {
                return Conflict(new { message = "Email already exist", success = false });
            }

            int result = await _userRepo.RegisterUser(user);

            if (result == 1)
            {
                return Ok(new { message = "User registered successfully", success = true });
            }
            else if (result == 0)
            {
                return BadRequest(new { message = "Problem occurred while registering user", success = false });
            }
            else if (result == -1)
            {
                return StatusCode(302, new { message = "Email Already Exist", success = false });
            }
            else
            {
                return StatusCode(500, new { message = "Unexpected error", success = false });
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginModel login)
        {
            System.Console.WriteLine(login.Identifier);
            System.Console.WriteLine(login.Password);
            return Ok(new { message = "Login Successfull", succeess = true });
        }


        [HttpGet("check-username")]
        public async Task<IActionResult> CheckUsername([FromQuery] string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest(new { available = false, message = "Username is required" });
            }
            username = username.Trim().ToLower();
            if (username.Length < 3)
            {
                return BadRequest(new { available = false, message = "Username must be at least 3 characters long" });
            }

            bool exists = await _userRepo.CheckUsername(username);

            if (exists)
            {
                return Ok(new { available = false, message = "Username is already taken" });
            }

            return Ok(new { available = true, message = "Username is available" });
        }
        [HttpGet("check-email")]
        public async Task<IActionResult> CheckEmail([FromQuery] string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest(new { available = false, message = "Email is required" });
            }
            email = email.Trim().ToLower();
            var emailValidator = new EmailAddressAttribute();
            if (!emailValidator.IsValid(email))
            {
                return BadRequest(new { available = false, message = "Invalid email format" });
            }

            bool exists = await _userRepo.CheckEmailExists(email);

            if (exists)
            {
                return Ok(new { available = false, message = "Email is already taken" });
            }

            return Ok(new { available = true, message = "Email is available" });
        }
    }
}
