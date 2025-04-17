using System.Security.Claims;
using System.Text;
using Infrastructure.EF;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using WebApi.Dto;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Policy = "Bearer")]
public class BooksController(UserManager<UserEntity> userManager) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        Console.WriteLine(GetCurrentUser().Email);
        return Ok(new
        {
            Title = "C#",
            Authorize = "Freeman"
        });
    }

    private UserEntity GetCurrentUser()
    {
        var ind = HttpContext.User.Identity as ClaimsIdentity;
        if (ind != null)
        {
            string? name= ind.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value;
            return userManager.FindByNameAsync(name).Result;
        }

        return null;
    }
    
    
}