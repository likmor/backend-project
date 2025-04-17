using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Infrastructure.EF;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Configuration;
using WebApi.Dto;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(
    SignInManager<UserEntity> signInManager,
    UserManager<UserEntity> userManager,
    JwtSettings jwtSettings)
    : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var user = await userManager.FindByNameAsync(dto.Username);
        if (user == null)
        {
            return BadRequest("Invalid username or password");
        }

        var result = await signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
        if (result.Succeeded)
        {
            return Ok(new { token = CreateToken(user) });
        }
        else
        {
            return BadRequest("Invalid username or password");
        }
    }

    private string CreateToken(UserEntity user)
    {
        return new JwtBuilder()
            .WithAlgorithm(new HMACSHA256Algorithm())
            .WithSecret(Encoding.UTF8.GetBytes(jwtSettings.Secret))
            .AddClaim(JwtRegisteredClaimNames.Name, user.UserName)
            .AddClaim(JwtRegisteredClaimNames.Gender, "male")
            .AddClaim(JwtRegisteredClaimNames.Email, user.Email)
            .AddClaim(JwtRegisteredClaimNames.Exp, DateTimeOffset.UtcNow.AddMinutes(5).ToUnixTimeSeconds())
            .AddClaim(JwtRegisteredClaimNames.Jti, Guid.NewGuid())
            .Audience(jwtSettings.Audience)
            .Issuer(jwtSettings.Issuer)
            .Encode();
    }
}