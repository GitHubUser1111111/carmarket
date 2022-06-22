using Carmarket.Domain.Identity;
using IdentityModel;
using IdentityServerHost.Quickstart.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace Carmarket.Application.Identity.IS4.Quickstart.Users
{
    [Route("user")]
    [Authorize(LocalApi.PolicyName)]
    public class UserController : Controller
    {
        private readonly UserManager<CarmarketIdentityUser> _userManager;

        public UserController(
            UserManager<CarmarketIdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] RegisterViewModel model)
        {
            var user = new CarmarketIdentityUser
            {
                UserName = model.UserName,
                Email = model.Email,
                EmailConfirmed = model.EmailConfirmed,
                PhoneNumber = model.PhoneNumber,
                SubjectId = model.SubjectId
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            result = await _userManager.AddClaimsAsync(user, new Claim[]{
                            new Claim(JwtClaimTypes.Email, model.Email),
                            new Claim(JwtClaimTypes.PhoneNumber, model.PhoneNumber),
                            new Claim(JwtClaimTypes.PreferredUserName, model.UserName)
                        });

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            return new OkResult();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
                return new NoContentResult();

            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            return new OkResult();
        }

        [HttpPut]
        public async Task<IActionResult> PutUser([FromBody] EditViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
                return new NoContentResult();

            user.UserName = model.UserName;
            user.Email = model.Email;
            user.EmailConfirmed = model.EmailConfirmed;
            user.PhoneNumber = model.PhoneNumber;
            user.SubjectId = model.SubjectId;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            return new OkResult();
        }
    }
}
