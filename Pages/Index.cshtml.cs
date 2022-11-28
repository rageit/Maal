using Maal.Data;
using Maal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Maal.Pages;

public class IndexModel : PageModel
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<IndexModel> _logger;
    private MaalContext _context;

    [BindProperty]
    public Game CurrentGame { get; set; } = default!;

    public IndexModel(ILogger<IndexModel> logger, MaalContext context, IConfiguration configuration)
    {
        _context = context;
        _logger = logger;
        _configuration = configuration;
    }

    public void OnGet()
    {
        CurrentGame = _context.Games.OrderByDescending(g => g.TimeStamp).FirstOrDefault();
    }
}
