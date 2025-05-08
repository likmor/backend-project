using Infrastructure.Dto;
using Infrastructure.Entities;

public interface IRestaurantService
{
    Task<List<RestaurantDto>> GetAllAsync(string? name, string? sortBy, bool descending);
    Task<RestaurantDto?> GetByIdAsync(int id);
    Task<RestaurantCreateDto> CreateAsync(RestaurantCreateDto dto);
}