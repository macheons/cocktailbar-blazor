// === FavoriteServiceTests.cs ===
using CocktailBar.Models;
using CocktailBar.Services;
using Xunit;

namespace CocktailBar.Tests;

public class FavoriteServiceTests
{
    /// Helper pour créer un cocktail de test
    private static CocktailDetail MakeCocktail(string id = "1", string name = "Mojito")
    {
        return new CocktailDetail
        {
            IdDrink = id,
            StrDrink = name,
            StrCategory = "Cocktail",
            StrAlcoholic = "Alcoholic",
            StrDrinkThumb = "https://example.com/mojito.jpg"
        };
    }

    [Fact]
    public void Test1_Add_ShouldAddCocktailToFavorites()
    {
        // Arrange
        var service = new FavoriteService();
        var cocktail = MakeCocktail();

        // Act
        service.Add(cocktail);

        // Assert
        Assert.Equal(1, service.GetAll().Count);
        Assert.True(service.IsFavorite("1"));
    }

    [Fact]
    public void Test2_Add_Duplicate_ShouldNotAddTwice()
    {
        // Arrange
        var service = new FavoriteService();
        var cocktail = MakeCocktail();

        // Act
        service.Add(cocktail);
        service.Add(cocktail);

        // Assert
        Assert.Equal(1, service.Count);
    }

    [Fact]
    public void Test3_Remove_ShouldRemoveCocktail()
    {
        // Arrange
        var service = new FavoriteService();
        var cocktail = MakeCocktail();
        service.Add(cocktail);

        // Act
        service.Remove("1");

        // Assert
        Assert.Equal(0, service.Count);
        Assert.False(service.IsFavorite("1"));
    }

    [Fact]
    public void Test4_Update_ShouldChangeCustomNameAndNotesAndRating()
    {
        // Arrange
        var service = new FavoriteService();
        var cocktail = MakeCocktail();
        service.Add(cocktail);

        // Act
        service.Update("1", "Mon Mojito", "Très frais", 5);

        // Assert
        var fav = service.GetById("1");
        Assert.NotNull(fav);
        Assert.Equal("Mon Mojito", fav.CustomName);
        Assert.Equal("Très frais", fav.PersonalNotes);
        Assert.Equal(5, fav.Rating);
    }

    [Fact]
    public void Test5_DisplayName_ShouldReturnCustomName_WhenSet()
    {
        // Arrange
        var fav = new Favorite
        {
            OriginalName = "Mojito",
            CustomName = "Custom"
        };

        // Act & Assert
        Assert.Equal("Custom", fav.DisplayName);
    }

    [Fact]
    public void Test6_DisplayName_ShouldReturnOriginalName_WhenCustomNameEmpty()
    {
        // Arrange
        var fav = new Favorite
        {
            OriginalName = "Mojito",
            CustomName = ""
        };

        // Act & Assert
        Assert.Equal("Mojito", fav.DisplayName);
    }
}
