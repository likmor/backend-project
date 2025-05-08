using Infrastructure.Entities;

namespace Infrastructure.Dto;

public class RestaurantCreateDto
{
    public string Name { get; set; }
    public string Country { get; set; }
    public string City { get; set; }

    public static Restaurant ToRestaurant(RestaurantCreateDto restaurant)
    {
        return new Restaurant()
        {
            Name = restaurant.Name,
            Country = restaurant.Country,
            City = restaurant.City,
        };
    }
}