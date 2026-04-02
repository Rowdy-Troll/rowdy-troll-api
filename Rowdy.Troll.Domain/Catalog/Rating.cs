namespace Rowdy.Troll.Domain.Catalog;

public class Rating
{
    public int Stars { get; set; }
    public string UserName { get; set; }
    public string Review { get; set; }

    public Rating(int stars, string username, string review)
    {
        if (stars < 1 || stars > 5)
        {
            throw new ArgumentException("Star rating must be an integer of 1, 2, 3, 4, 5.");
        }

        if (string.IsNullOrEmpty(username))
        {
            throw new ArgumentException("UserName cannot be null.");
        }

        Stars = stars;
        UserName = username;
        Review = review;
    }
}