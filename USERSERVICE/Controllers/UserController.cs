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
                return Conflict(new { message = "Email Already Exist", success = false });
            }
            else
            {
                return StatusCode(500, new { message = "Unexpected error", success = false });
            }
        }
    }
}
