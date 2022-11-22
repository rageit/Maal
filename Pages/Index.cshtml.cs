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
    public List<Player> Players { get; set; } = default!;

    public IndexModel(ILogger<IndexModel> logger, MaalContext context, IConfiguration configuration)
    {
        _context = context;
        _logger = logger;
        _configuration = configuration;
    }

    public void OnGet()
    {
        ViewData["DbPath"] = _context.DbPath;
        Players = _context.Players.ToList();
    }
}
