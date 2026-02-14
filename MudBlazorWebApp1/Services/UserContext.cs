using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace MudBlazorWebApp1.Services;

public class UserContext
{
    private readonly AuthenticationStateProvider _auth;

    public UserContext(AuthenticationStateProvider auth) => _auth = auth;

    public async Task<string> GetUserIdAsync()
    {
        var state = await _auth.GetAuthenticationStateAsync();
        var user = state.User;

        var userId =
            user.FindFirstValue(ClaimTypes.NameIdentifier) ??
            user.FindFirstValue("sub");

        if (string.IsNullOrWhiteSpace(userId))
            throw new InvalidOperationException("User is not authenticated.");

        return userId;
    }
}