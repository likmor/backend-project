using System.Security.Claims;
using System.Text;
using Infrastructure.Dto;
using Infrastructure.EF;
using Infrastructure.Entities;
using Infrastructure.Services;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
// [Authorize(Policy = "Bearer")]
public class RestaurantsController(UserManager<UserEntity> userManager, IRestaurantService _service) : ControllerBase
{
    [HttpGet]
    [Route("GetAll")]
    public async Task<IActionResult> GetAll(string? name, string? sortBy, bool descending = true)
    {
        var restaurants = await _service.GetAllAsync(name, sortBy, descending);
        return Ok(restaurants);
    }
    [HttpGet]
    [Route("GetById")]
    public async Task<IActionResult> GetById(int id)
    {
        var restaurant = await _service.GetByIdAsync(id);
        if (restaurant == null)
        {
            return NotFound();
        }
        return Ok(restaurant);
    }

    [HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create([FromBody] RestaurantCreateDto dto)
    {
        var restaurant = await _service.CreateAsync(dto);
        return Ok(restaurant);
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