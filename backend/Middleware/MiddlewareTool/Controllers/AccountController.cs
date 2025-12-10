using Microsoft.AspNetCore.Mvc;

namespace MiddlewareTool.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        public AccountController()
        {
        }

        [HttpPost(Name = "login")]
        public IActionResult Login()
        {
            return Ok(new { Token = Guid.NewGuid().ToString(), Message = "" });
        }
    }
}
