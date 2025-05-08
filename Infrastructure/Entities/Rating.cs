using System;
using System.Collections.Generic;

namespace Infrastructure.Entities;

public partial class Rating
{
    public int Id { get; set; }

    public string ConsumerId { get; set; } = null!;

    public int RestaurantId { get; set; }

    public byte OverallRating { get; set; }

    public byte FoodRating { get; set; }

    public byte ServiceRating { get; set; }

    public virtual Restaurant Restaurant { get; set; } = null!;
}
