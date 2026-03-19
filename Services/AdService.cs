namespace Maal.Services;

public interface IAdService
{
    bool IsAdFree(HttpContext httpContext);
    void SetAdFree(HttpContext httpContext);
}

public class AdService : IAdService
{
    private const string CookieName = "MaalAdFree";

    public bool IsAdFree(HttpContext httpContext)
    {
        return httpContext.Request.Cookies.TryGetValue(CookieName, out var value) && value == "true";
    }

    public void SetAdFree(HttpContext httpContext)
    {
        httpContext.Response.Cookies.Append(CookieName, "true", new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            MaxAge = TimeSpan.FromDays(365 * 10),
            IsEssential = true
        });
    }
}
