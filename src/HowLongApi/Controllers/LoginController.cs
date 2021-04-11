using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using HowLongApi.Models;
using HowLongApi.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using static System.Environment;

namespace HowLongApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        public LoginController(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Token(Login model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.User, model.Password, true, true);
                if (result.Succeeded)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, model.User),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    };

                    var key = GetEnvironmentVariable("key");

                    var chave = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(key));
                    var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                            issuer: "HowLong",
                            audience: "Users",
                            claims: claims,
                            signingCredentials: credenciais,
                            expires: DateTime.Now.AddMinutes(30)
                        );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                    return Ok(tokenString);
                }
                return Unauthorized();
            }
            return BadRequest();
        }
    }
}