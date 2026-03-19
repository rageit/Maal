using Maal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Maal.Pages;

public class RemoveAdsModel : PageModel
{
    private readonly IAdService _adService;

    public bool IsAdFree { get; set; }

    public RemoveAdsModel(IAdService adService)
    {
        _adService = adService;
    }

    public void OnGet()
    {
        IsAdFree = _adService.IsAdFree(HttpContext);
    }

    public IActionResult OnPost()
    {
        // TODO: Integrate with a real payment provider (e.g. Stripe) here.
        // For now, this simulates a successful purchase.
        _adService.SetAdFree(HttpContext);
        return RedirectToPage();
    }
}
