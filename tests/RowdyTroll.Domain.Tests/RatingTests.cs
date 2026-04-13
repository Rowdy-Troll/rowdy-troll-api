using Microsoft.VisualStudio.TestTools.UnitTesting;
using RowdyTroll.Domain.Catalog;

namespace RowdyTroll.Domain.Tests;

[TestClass]
public class RatingTests
{
    [TestMethod]
    public void Constructor_WithValidParameters_SetsProperties()
    {
        // Arrange & Act
        var rating = new Rating(5, "alice", "Great product");

        // Assert
        Assert.AreEqual(5, rating.Stars);
        Assert.AreEqual("alice", rating.UserName);
        Assert.AreEqual("Great product", rating.Review);
    }

    [TestMethod]
    public void Constructor_WithInvalidStars_ThrowsArgumentOutOfRangeException()
    {
        // Arrange, Act & Assert
        Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => new Rating(0, "bob"));
        Assert.ThrowsException<System.ArgumentOutOfRangeException>(() => new Rating(6, "bob"));
    }

    [TestMethod]
    public void Constructor_WithEmptyUserName_ThrowsArgumentException()
    {
        Assert.ThrowsException<System.ArgumentException>(() => new Rating(3, ""));
        Assert.ThrowsException<System.ArgumentException>(() => new Rating(3, "   "));
    }
}
