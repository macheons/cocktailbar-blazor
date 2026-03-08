// === Favorite.cs ===
namespace CocktailBar.Models;

public class Favorite
{
    public string CocktailId { get; set; } = string.Empty;
    public string OriginalName { get; set; } = string.Empty;
    public string CustomName { get; set; } = string.Empty;
    public string PersonalNotes { get; set; } = string.Empty;
    public int Rating { get; set; } = 0;
    public string ImageUrl { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public bool IsAlcoholic { get; set; }
    public DateTime AddedAt { get; set; } = DateTime.Now;

    public string DisplayName => string.IsNullOrWhiteSpace(CustomName) ? OriginalName : CustomName;
    public string RatingStars => new string('\u2605', Rating) + new string('\u2606', 5 - Rating);
}
