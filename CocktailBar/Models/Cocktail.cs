// === Cocktail.cs ===
using System.Text.Json.Serialization;

namespace CocktailBar.Models;

/// Réponse brute de l'API : { "drinks": [...] }
public class CocktailApiResponse
{
    [JsonPropertyName("drinks")]
    public List<CocktailDetail>? Drinks { get; set; }
}

/// Réponse filtrée (filter.php) : champs limités
public class CocktailFilterResponse
{
    [JsonPropertyName("drinks")]
    public List<CocktailSummary>? Drinks { get; set; }
}

/// Summary (résultat d'un filtre par catégorie)
public class CocktailSummary
{
    [JsonPropertyName("idDrink")]
    public string IdDrink { get; set; } = string.Empty;

    [JsonPropertyName("strDrink")]
    public string StrDrink { get; set; } = string.Empty;

    [JsonPropertyName("strDrinkThumb")]
    public string StrDrinkThumb { get; set; } = string.Empty;

    public int Id => int.TryParse(IdDrink, out var id) ? id : 0;
}

/// Détail complet d'un cocktail
public class CocktailDetail
{
    [JsonPropertyName("idDrink")]
    public string IdDrink { get; set; } = string.Empty;

    [JsonPropertyName("strDrink")]
    public string StrDrink { get; set; } = string.Empty;

    [JsonPropertyName("strCategory")]
    public string StrCategory { get; set; } = string.Empty;

    [JsonPropertyName("strAlcoholic")]
    public string StrAlcoholic { get; set; } = string.Empty;

    [JsonPropertyName("strGlass")]
    public string StrGlass { get; set; } = string.Empty;

    [JsonPropertyName("strInstructions")]
    public string StrInstructions { get; set; } = string.Empty;

    [JsonPropertyName("strDrinkThumb")]
    public string StrDrinkThumb { get; set; } = string.Empty;

    // Ingrédients 1 à 15
    [JsonPropertyName("strIngredient1")] public string? StrIngredient1 { get; set; }
    [JsonPropertyName("strIngredient2")] public string? StrIngredient2 { get; set; }
    [JsonPropertyName("strIngredient3")] public string? StrIngredient3 { get; set; }
    [JsonPropertyName("strIngredient4")] public string? StrIngredient4 { get; set; }
    [JsonPropertyName("strIngredient5")] public string? StrIngredient5 { get; set; }
    [JsonPropertyName("strIngredient6")] public string? StrIngredient6 { get; set; }
    [JsonPropertyName("strIngredient7")] public string? StrIngredient7 { get; set; }
    [JsonPropertyName("strIngredient8")] public string? StrIngredient8 { get; set; }
    [JsonPropertyName("strIngredient9")] public string? StrIngredient9 { get; set; }
    [JsonPropertyName("strIngredient10")] public string? StrIngredient10 { get; set; }
    [JsonPropertyName("strIngredient11")] public string? StrIngredient11 { get; set; }
    [JsonPropertyName("strIngredient12")] public string? StrIngredient12 { get; set; }
    [JsonPropertyName("strIngredient13")] public string? StrIngredient13 { get; set; }
    [JsonPropertyName("strIngredient14")] public string? StrIngredient14 { get; set; }
    [JsonPropertyName("strIngredient15")] public string? StrIngredient15 { get; set; }

    // Mesures 1 à 15
    [JsonPropertyName("strMeasure1")] public string? StrMeasure1 { get; set; }
    [JsonPropertyName("strMeasure2")] public string? StrMeasure2 { get; set; }
    [JsonPropertyName("strMeasure3")] public string? StrMeasure3 { get; set; }
    [JsonPropertyName("strMeasure4")] public string? StrMeasure4 { get; set; }
    [JsonPropertyName("strMeasure5")] public string? StrMeasure5 { get; set; }
    [JsonPropertyName("strMeasure6")] public string? StrMeasure6 { get; set; }
    [JsonPropertyName("strMeasure7")] public string? StrMeasure7 { get; set; }
    [JsonPropertyName("strMeasure8")] public string? StrMeasure8 { get; set; }
    [JsonPropertyName("strMeasure9")] public string? StrMeasure9 { get; set; }
    [JsonPropertyName("strMeasure10")] public string? StrMeasure10 { get; set; }
    [JsonPropertyName("strMeasure11")] public string? StrMeasure11 { get; set; }
    [JsonPropertyName("strMeasure12")] public string? StrMeasure12 { get; set; }
    [JsonPropertyName("strMeasure13")] public string? StrMeasure13 { get; set; }
    [JsonPropertyName("strMeasure14")] public string? StrMeasure14 { get; set; }
    [JsonPropertyName("strMeasure15")] public string? StrMeasure15 { get; set; }

    public int Id => int.TryParse(IdDrink, out var id) ? id : 0;

    /// Retourne la liste des ingrédients non-null avec leur mesure
    public List<CocktailIngredient> GetIngredients()
    {
        var ingredients = new List<CocktailIngredient>();
        var ingredientProps = new[]
        {
            StrIngredient1, StrIngredient2, StrIngredient3, StrIngredient4, StrIngredient5,
            StrIngredient6, StrIngredient7, StrIngredient8, StrIngredient9, StrIngredient10,
            StrIngredient11, StrIngredient12, StrIngredient13, StrIngredient14, StrIngredient15
        };
        var measureProps = new[]
        {
            StrMeasure1, StrMeasure2, StrMeasure3, StrMeasure4, StrMeasure5,
            StrMeasure6, StrMeasure7, StrMeasure8, StrMeasure9, StrMeasure10,
            StrMeasure11, StrMeasure12, StrMeasure13, StrMeasure14, StrMeasure15
        };

        for (int i = 0; i < 15; i++)
        {
            if (!string.IsNullOrWhiteSpace(ingredientProps[i]))
            {
                ingredients.Add(new CocktailIngredient
                {
                    Name = ingredientProps[i]!,
                    Measure = measureProps[i] ?? string.Empty,
                    ImageUrl = $"https://www.thecocktaildb.com/images/ingredients/{Uri.EscapeDataString(ingredientProps[i]!)}-Small.png"
                });
            }
        }
        return ingredients;
    }
}

/// Ingrédient avec image
public class CocktailIngredient
{
    public string Name { get; set; } = string.Empty;
    public string Measure { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}

/// Réponse pour list.php?c=list
public class CategoryListResponse
{
    [JsonPropertyName("drinks")]
    public List<CategoryItem>? Drinks { get; set; }
}

public class CategoryItem
{
    [JsonPropertyName("strCategory")]
    public string StrCategory { get; set; } = string.Empty;
}
