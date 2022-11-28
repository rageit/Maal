using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Maal.Pages;

public class NotFoundModel : PageModel
{
    private readonly ILogger<NotFoundModel> _logger;

    public NotFoundModel(ILogger<NotFoundModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}