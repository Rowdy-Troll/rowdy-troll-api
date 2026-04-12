using Microsoft.VisualStudio.TestTools.UnitTesting;
using RowdyTroll.Domain.Catalog;

namespace RowdyTroll.Domain.Tests;

[TestClass]
public class ItemTests
{
    [TestMethod]
    public void Constructor_WithValidParameters_InitializesValues()
    {
        var item = new Item("Widget", "A widget", "Acme", 9.99m);

        Assert.AreEqual("Widget", item.Name);
        Assert.AreEqual("A widget", item.Description);
        Assert.AreEqual("Acme", item.Brand);
        Assert.AreEqual(9.99m, item.Price);
        Assert.IsNotNull(item.Ratings);
        Assert.AreEqual(0, item.Ratings.Count);
    }

    [TestMethod]
    public void AddRating_AddsToRatings()
    {
        var item = new Item("Gadget", "A gadget", "Contoso", 1.23m);
        var rating = new Rating(4, "user1", "Nice");

        item.AddRating(rating);

        Assert.AreEqual(1, item.Ratings.Count);
        Assert.AreSame(rating, item.Ratings[0]);
    }

    [TestMethod]
    public void AddRating_Null_ThrowsArgumentNullException()
    {
        var item = new Item("Thing", "A thing", "Brand", 0m);
        Assert.ThrowsException<System.ArgumentNullException>(() => item.AddRating(null!));
    }
}
