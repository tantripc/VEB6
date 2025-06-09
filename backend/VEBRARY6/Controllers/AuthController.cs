using Microsoft.AspNetCore.Mvc;
using VEBRARY6.Model.User;

namespace VEBRARY6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel request)
        {
            return LoginLogic(request);
        }
        private IActionResult LoginLogic(LoginModel request)
        {
            // Dummy authentication logic
            if (request.Username == "admin" && request.Password == "123456")
            {
                var permissions = new[] { "read", "write", "delete", "admin" };

                // Normally, generate and return a JWT or similar token here
                return Ok(new { token = "dummy-token", permissions = permissions });
            }
            return Unauthorized();
        }
    }
}