using Microsoft.AspNetCore.Authorization;

namespace MiddlewareTool.Controllers.Base
{
    [Authorize]
    public class BaseAuthController : BaseController
    {
        public BaseAuthController(IConfiguration config) : base(config)
        {
        }
    }
}
