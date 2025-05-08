using Infrastructure.Entities;

namespace Infrastructure.Dto;

public class RestaurantDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    
    public string OverallRating { get; set; }

    public string FoodRating { get; set; }

    public string ServiceRating { get; set; }

    public static RestaurantDto FromRestaurant(Restaurant restaurant)
    {
        return new RestaurantDto()
        {
            Id = restaurant.RestaurantId,
            Name = restaurant.Name,
            Address = $"{restaurant.Country}, {restaurant.City}",
            OverallRating = restaurant.Ratings.Count != 0 ? restaurant.Ratings.Average(r => r.OverallRating).ToString("0.0") : "0",
            FoodRating = restaurant.Ratings.Count != 0 ? restaurant.Ratings.Average(r => r.FoodRating).ToString("0.0") : "0",
            ServiceRating = restaurant.Ratings.Count != 0 ? restaurant.Ratings.Average(r => r.ServiceRating).ToString("0.0") : "0",
        };
    }
}