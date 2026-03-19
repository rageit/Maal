namespace Maal.Services;

public interface IUserIdentificationService
{
    string GetUserId(HttpContext httpContext);
}

public class UserIdentificationService : IUserIdentificationService
{
    private const string CookieName = "MaalUserId";

    public string GetUserId(HttpContext httpContext)
    {
        if (httpContext.Request.Cookies.TryGetValue(CookieName, out var userId) && !string.IsNullOrEmpty(userId))
        {
            return userId;
        }

        userId = Guid.NewGuid().ToString("N");
        httpContext.Response.Cookies.Append(CookieName, userId, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            MaxAge = TimeSpan.FromDays(365 * 5),
            IsEssential = true
        });

        return userId;
    }
}
