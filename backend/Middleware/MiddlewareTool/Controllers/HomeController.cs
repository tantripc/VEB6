using Microsoft.AspNetCore.Mvc;
using MiddlewareTool.Controllers.Base;

namespace MiddlewareTool.Controllers
{
    public class HomeController : BaseAuthController
    {
        public HomeController(IConfiguration config): base(config)
        {
        }

        [HttpGet(Name = "index")]
        public IActionResult Index()
        {
            var fullName = User.FindFirst("FullName")?.Value;
            return Ok(new { fullName, message = "Connection successful" });
        }
    }
}
