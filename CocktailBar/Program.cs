// === Program.cs ===
using CocktailBar.Components;
using CocktailBar.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// HttpClient pour TheCocktailDB
builder.Services.AddHttpClient<CocktailService>();

// Services Singleton (état partagé en mémoire)
builder.Services.AddSingleton<FavoriteService>();
builder.Services.AddSingleton<AuthService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

// Nécessaire pour les tests d'intégration
public partial class Program { }
