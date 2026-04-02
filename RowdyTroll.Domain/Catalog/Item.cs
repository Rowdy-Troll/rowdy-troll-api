using System;
using System.Collections.Generic;

namespace RowdyTroll.Domain.Catalog
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public List<Rating> Ratings { get; set; }

        public Item(string name, string description, string brand, decimal price)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be null or empty", nameof(name));
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be null or empty", nameof(description));
            if (string.IsNullOrWhiteSpace(brand))
                throw new ArgumentException("Brand cannot be null or empty", nameof(brand));
            if (price < 0)
                throw new ArgumentOutOfRangeException(nameof(price), "Price must be >= 0");

            Name = name;
            Description = description;
            Brand = brand;
            Price = price;
            Ratings = new List<Rating>();
        }

        // Parameterless constructor for model binding / serializers
        public Item()
        {
            Ratings = new List<Rating>();
            Name = string.Empty;
            Description = string.Empty;
            Brand = string.Empty;
            Price = 0m;
        }

        public void AddRating(Rating rating)
        {
            if (rating == null) throw new ArgumentNullException(nameof(rating));
            Ratings.Add(rating);
        }
    }
}
