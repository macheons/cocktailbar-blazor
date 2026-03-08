// === FavoriteService.cs ===
using CocktailBar.Models;

namespace CocktailBar.Services;

public class FavoriteService
{
    private readonly List<Favorite> _favorites = new();

    public event Action? OnChange;

    public IReadOnlyList<Favorite> GetAll() => _favorites.AsReadOnly();

    public int Count => _favorites.Count;

    public bool IsFavorite(string cocktailId)
    {
        return _favorites.Any(f => f.CocktailId == cocktailId);
    }

    public Favorite? GetById(string cocktailId)
    {
        return _favorites.FirstOrDefault(f => f.CocktailId == cocktailId);
    }

    public void Add(CocktailDetail cocktail)
    {
        if (IsFavorite(cocktail.IdDrink))
            return;

        _favorites.Add(new Favorite
        {
            CocktailId = cocktail.IdDrink,
            OriginalName = cocktail.StrDrink,
            CustomName = string.Empty,
            PersonalNotes = string.Empty,
            Rating = 0,
            ImageUrl = cocktail.StrDrinkThumb,
            Category = cocktail.StrCategory,
            IsAlcoholic = cocktail.StrAlcoholic == "Alcoholic",
            AddedAt = DateTime.Now
        });
        OnChange?.Invoke();
    }

    public void Update(string cocktailId, string customName, string personalNotes, int rating)
    {
        var fav = GetById(cocktailId);
        if (fav == null) return;

        fav.CustomName = customName;
        fav.PersonalNotes = personalNotes;
        fav.Rating = Math.Clamp(rating, 0, 5);
        OnChange?.Invoke();
    }

    public void Remove(string cocktailId)
    {
        var fav = GetById(cocktailId);
        if (fav == null) return;

        _favorites.Remove(fav);
        OnChange?.Invoke();
    }
}
