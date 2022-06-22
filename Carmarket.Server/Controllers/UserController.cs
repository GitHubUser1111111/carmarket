using Carmarket.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Carmarket.Server.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ILogger<UserController> _logger;

        public UserController(SignInManager<IdentityUser> signInManager, ILogger<UserController> logger)
        {
            this.signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<User> GetCurrentUser()
        {
            return await Task.FromResult(new User() { FirstName = "Kimi", LastName = "Raikkonen", UserName = "Iceman" });
        }
    }
}