using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        [HttpGet("GetPosts/{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            
            return Ok(new { message = "Ok got itt", id });
        }

    }
}
