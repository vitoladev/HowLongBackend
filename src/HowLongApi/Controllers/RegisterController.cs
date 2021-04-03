using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using HowLongApi.Models;
using HowLongApi.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HowLongApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        public RegisterController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] Login model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new User
                    {
                        UserName = model.User,
                        Email = model.User
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        return Created("", result);
                    }
                    else
                    {
                        return BadRequest(result);
                    }
                }
                catch (Exception e)
                {
                    return StatusCode(500, $"error on trying to register: {e}");
                }
            }
            else
            {
                return BadRequest();
            }
        }
    }
}