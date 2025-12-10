using Microsoft.AspNetCore.Mvc;

namespace MiddlewareTool.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController : ControllerBase
    {
        protected readonly IConfiguration _config;
        public BaseController(IConfiguration config)
        {
            _config = config;
        }
    }
}
