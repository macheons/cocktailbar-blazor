// === AuthService.cs ===
namespace CocktailBar.Services;

public class AuthService
{
    // Comptes en mémoire
    private readonly Dictionary<string, string> _users = new()
    {
        { "james", "bond007" },
        { "martini", "shaken" },
        { "bar", "tender" }
    };

    public string? CurrentUser { get; private set; }
    public bool IsAuthenticated => CurrentUser != null;

    public event Action? OnAuthChanged;

    public bool Login(string username, string password)
    {
        var key = username.ToLower();
        if (_users.TryGetValue(key, out var storedPassword) && storedPassword == password)
        {
            CurrentUser = key;
            OnAuthChanged?.Invoke();
            return true;
        }
        return false;
    }

    public void Logout()
    {
        CurrentUser = null;
        OnAuthChanged?.Invoke();
    }
}
