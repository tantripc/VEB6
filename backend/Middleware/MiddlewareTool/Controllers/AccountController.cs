using Microsoft.AspNetCore.Mvc;

namespace MiddlewareTool.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "login")]
        public IActionResult Login()
        {
            return Ok(new { Token = Guid.NewGuid().ToString(), Message = "" });
        }
    }
}
