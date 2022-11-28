using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Maal.Data;
using Maal.Models;
using Microsoft.EntityFrameworkCore;

namespace Maal.Pages;

public class GamesModel : PageModel
{
    private readonly ILogger<GamesModel> _logger;
    private MaalContext _context;

    [BindProperty]
    public List<Game> Games { get; set; } = default!;

    public GamesModel(ILogger<GamesModel> logger, MaalContext context)
    {
        _context = context;
        _logger = logger;
    }

    public void OnGet()
    {
        Games = _context.Games
            .Include(x => x.Rounds)
            .OrderBy(x => x.TimeStamp)
            .ToList();
    }
}