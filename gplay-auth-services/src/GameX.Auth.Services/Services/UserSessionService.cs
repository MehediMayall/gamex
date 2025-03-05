using System.Text.Json;

namespace gamex.Auth.Services;

public sealed class UserSessionService : IUserSessionService
{
    private readonly IHttpContextAccessor _httpContext;

    public UserSessionService(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }

    public UserSessionDto Get()
    {

        if (_httpContext is null || _httpContext.HttpContext is null) 
            return null;

        var httpContext = _httpContext.HttpContext ?? 
        throw new ArgumentNullException(nameof(HttpContext));
        
        var userClaims = httpContext?.User?.Claims;
        
        if (userClaims is null || !userClaims.Any())
            // throw new UnauthorizedAccessException("INVALID_CREDENTIALS");
            return null;
    
        var claim = userClaims.FirstOrDefault() ??
            throw new UnauthorizedAccessException("Login failed. Please try again. Claim object is empty.");

        return claim.Value != null ? JsonSerializer.Deserialize<UserSessionDto>(claim.Value) : null;
    }
}
