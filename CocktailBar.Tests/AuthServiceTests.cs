// === AuthServiceTests.cs ===
using CocktailBar.Services;
using Xunit;

namespace CocktailBar.Tests;

public class AuthServiceTests
{
    [Fact]
    public void Test1_Login_ValidCredentials_ShouldReturnTrue_AndSetCurrentUser()
    {
        // Arrange
        var service = new AuthService();

        // Act
        var result = service.Login("james", "bond007");

        // Assert
        Assert.True(result);
        Assert.Equal("james", service.CurrentUser);
        Assert.True(service.IsAuthenticated);
    }

    [Fact]
    public void Test2_Login_InvalidCredentials_ShouldReturnFalse()
    {
        // Arrange
        var service = new AuthService();

        // Act
        var result = service.Login("james", "wrongpassword");

        // Assert
        Assert.False(result);
        Assert.Null(service.CurrentUser);
        Assert.False(service.IsAuthenticated);
    }

    [Fact]
    public void Test3_Logout_ShouldSetCurrentUserToNull_AndIsAuthenticatedFalse()
    {
        // Arrange
        var service = new AuthService();
        service.Login("james", "bond007");

        // Act
        service.Logout();

        // Assert
        Assert.Null(service.CurrentUser);
        Assert.False(service.IsAuthenticated);
    }

    [Fact]
    public void Test4_Login_CaseInsensitiveUsername_ShouldWork()
    {
        // Arrange
        var service = new AuthService();

        // Act
        var result = service.Login("JAMES", "bond007");

        // Assert
        Assert.True(result);
        Assert.Equal("james", service.CurrentUser);
    }
}
