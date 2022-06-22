using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Carmarket.Microservice.Users.Controllers
{
    [ApiController]
    [Route("admin")]
    [Authorize(Policy = "AdminOnly")]
    public class AdminController : ControllerBase
    {
        private readonly ILogger<AdminController> logger;

        public AdminController(ILogger<AdminController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        [Route("whoami")]
        public IActionResult WhoAmI()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}