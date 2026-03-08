// === CocktailService.cs ===
using System.Net.Http.Json;
using CocktailBar.Models;

namespace CocktailBar.Services;

public class CocktailService
{
    private readonly HttpClient _http;
    private const string BaseUrl = "https://www.thecocktaildb.com/api/json/v1/1/";

    public CocktailService(HttpClient http)
    {
        _http = http;
        _http.BaseAddress = new Uri(BaseUrl);
    }

    public async Task<List<CocktailDetail>> SearchCocktailsAsync(string query)
    {
        var response = await _http.GetFromJsonAsync<CocktailApiResponse>($"search.php?s={Uri.EscapeDataString(query)}");
        return response?.Drinks ?? new List<CocktailDetail>();
    }

    public async Task<CocktailDetail?> GetCocktailByIdAsync(string id)
    {
        var response = await _http.GetFromJsonAsync<CocktailApiResponse>($"lookup.php?i={id}");
        return response?.Drinks?.FirstOrDefault();
    }

    public async Task<CocktailDetail?> GetRandomCocktailAsync()
    {
        var response = await _http.GetFromJsonAsync<CocktailApiResponse>("random.php");
        return response?.Drinks?.FirstOrDefault();
    }

    public async Task<List<string>> GetCategoriesAsync()
    {
        var response = await _http.GetFromJsonAsync<CategoryListResponse>("list.php?c=list");
        return response?.Drinks?.Select(c => c.StrCategory).ToList() ?? new List<string>();
    }

    public async Task<List<CocktailSummary>> FilterByCategoryAsync(string category)
    {
        var response = await _http.GetFromJsonAsync<CocktailFilterResponse>($"filter.php?c={Uri.EscapeDataString(category)}");
        return response?.Drinks ?? new List<CocktailSummary>();
    }
}
