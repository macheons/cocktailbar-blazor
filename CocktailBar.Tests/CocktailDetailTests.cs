// === CocktailDetailTests.cs ===
using Bunit;
using CocktailBar.Models;
using CocktailBar.Services;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using CocktailDetailPage = CocktailBar.Components.Pages.CocktailDetailPage;

namespace CocktailBar.Tests;

public class CocktailDetailTests : TestContext
{
    /// Configure les services de test avec un AuthService et FavoriteService
    private void SetupServices(bool login = false)
    {
        var authService = new AuthService();
        if (login)
            authService.Login("james", "bond007");

        Services.AddSingleton(authService);
        Services.AddSingleton(new FavoriteService());

        // Enregistrer un HttpClient pour CocktailService
        Services.AddHttpClient<CocktailService>();
    }

    [Fact]
    public void Test1_FavoriteButton_NotVisible_WhenUserNotAuthenticated()
    {
        // Arrange
        SetupServices(login: false);

        // Act
        var cut = RenderComponent<CocktailDetailPage>(parameters =>
            parameters.Add(p => p.Id, "11007"));

        // Assert — pas de bouton "Ajouter aux favoris"
        Assert.DoesNotContain("Ajouter aux favoris", cut.Markup);
        // Assert — texte "Connectez-vous" visible
        Assert.Contains("Connectez-vous", cut.Markup);
    }

    [Fact]
    public void Test2_FavoriteButton_Visible_WhenUserAuthenticated()
    {
        // Arrange
        SetupServices(login: true);

        // Act
        var cut = RenderComponent<CocktailDetailPage>(parameters =>
            parameters.Add(p => p.Id, "11007"));

        // Assert — bouton favori présent (texte contient "favoris")
        Assert.Contains("favoris", cut.Markup.ToLower());
    }

    [Fact]
    public void Test3_LoginHint_ContainsLinkToLoginPage()
    {
        // Arrange
        SetupServices(login: false);

        // Act
        var cut = RenderComponent<CocktailDetailPage>(parameters =>
            parameters.Add(p => p.Id, "11007"));

        // Assert — lien vers /login présent
        var loginLink = cut.Find("a[href='/login']");
        Assert.NotNull(loginLink);
    }
}
