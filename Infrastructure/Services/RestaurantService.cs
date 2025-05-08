using Infrastructure.Dto;
using Infrastructure.EF;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class RestaurantService : IRestaurantService
{
    private readonly AppDbContext _context;

    public RestaurantService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<RestaurantDto>> GetAllAsync(string? name, string? sortBy, bool descending)
    {
        var restaurants = await _context.Restaurants
            .Include(r => r.Ratings)
            .Select(r => RestaurantDto.FromRestaurant(r))
            .ToListAsync();
        if (!string.IsNullOrEmpty(name))
        {
            restaurants = restaurants.Where(r => r.Name.Contains(name, StringComparison.InvariantCultureIgnoreCase))
                .ToList();
        }

        if (string.IsNullOrEmpty(sortBy))
        {
            sortBy = "overallrating";
        }
        restaurants = sortBy.ToLower() switch
        {
            "overallrating" => descending
                ? restaurants.OrderByDescending(r => r.OverallRating).ThenBy(r => r.Name).ToList()
                : restaurants.OrderBy(r => r.OverallRating).ThenBy(r => r.Name).ToList(),
            "foodrating" => descending
                ? restaurants.OrderByDescending(r => r.FoodRating).ThenBy(r => r.Name).ToList()
                : restaurants.OrderBy(r => r.FoodRating).ThenBy(r => r.Name).ToList(),
            "servicerating" => descending
                ? restaurants.OrderByDescending(r => r.ServiceRating).ThenBy(r => r.Name).ToList()
                : restaurants.OrderBy(r => r.ServiceRating).ThenBy(r => r.Name).ToList(),
            _ => descending
                ? restaurants.OrderByDescending(r => r.OverallRating).ThenBy(r => r.Name).ToList()
                : restaurants.OrderBy(r => r.OverallRating).ThenBy(r => r.Name).ToList()
        };

        return restaurants;
    }
    public async Task<RestaurantDto?> GetByIdAsync(int id)
    {
        var restaurant = await _context.Restaurants
            .Include(r => r.Ratings)
            .FirstOrDefaultAsync(r => r.RestaurantId == id);
        return restaurant == null ? null : RestaurantDto.FromRestaurant(restaurant);
    }

    public async Task<RestaurantCreateDto> CreateAsync(RestaurantCreateDto dto)
    {
        var restaurant = RestaurantCreateDto.ToRestaurant(dto);
        _context.Restaurants.Add(restaurant);
        await _context.SaveChangesAsync();
        return dto;
    }
}