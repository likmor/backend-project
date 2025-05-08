using System;
using System.Collections.Generic;

namespace Infrastructure.Entities;

public partial class Restaurant
{
    public int RestaurantId { get; set; }

    public string Name { get; set; } = null!;

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public string? ZipCode { get; set; }

    public string? Latitude { get; set; }

    public string? Longitude { get; set; }

    public string? AlcoholService { get; set; }

    public string? SmokingAllowed { get; set; }

    public string? Price { get; set; }

    public string? Franchise { get; set; }

    public string? Area { get; set; }

    public string? Parking { get; set; }

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}
