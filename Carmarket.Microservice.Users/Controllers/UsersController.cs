using Carmarket.Domain.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Carmarket.Microservice.Users.Controllers
{
    [ApiController]
    [Route("users")]
    [Authorize(Policy = "AdminOnly")]
    public class UsersController
    {
        private readonly UserManager<CarmarketIdentityUser> userManager;
        private readonly ILogger<UsersController> logger;

        public UsersController(UserManager<CarmarketIdentityUser> userManager, ILogger<UsersController> logger)
        {
            this.userManager = userManager;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var users = userManager.Users.ToList();
            return new JsonResult(users);
        }
    }
}
