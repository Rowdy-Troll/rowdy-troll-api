using System;

namespace RowdyTroll.Domain.Catalog
{
    public class Rating
    {
        public int Id { get; set; }

        public int Stars { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Review { get; set; } = string.Empty;

        public Rating(int stars, string userName, string review = "")
        {
            if (stars < 1 || stars > 5)
                throw new ArgumentOutOfRangeException(nameof(stars), "Stars must be between 1 and 5");
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("UserName cannot be null or empty", nameof(userName));

            Stars = stars;
            UserName = userName;
            Review = review ?? string.Empty;
        }

        // Parameterless constructor for model binding / serializers
        public Rating() { }
    }
}
