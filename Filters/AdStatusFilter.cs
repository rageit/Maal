using Maal.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Maal.Filters;

public class AdStatusFilter : IPageFilter
{
    private readonly IAdService _adService;

    public AdStatusFilter(IAdService adService)
    {
        _adService = adService;
    }

    public void OnPageHandlerSelected(PageHandlerSelectedContext context) { }

    public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
    {
        if (context.HandlerInstance is PageModel page)
        {
            page.ViewData["HideAds"] = _adService.IsAdFree(context.HttpContext);
        }
    }

    public void OnPageHandlerExecuted(PageHandlerExecutedContext context) { }
}
