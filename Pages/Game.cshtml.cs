using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Maal.Data;
using Maal.Models;
using Microsoft.EntityFrameworkCore;

namespace Maal.Pages;

public class GameModel : PageModel
{
    private readonly ILogger<GameModel> _logger;
    private MaalContext _context;

    [BindProperty]
    public Game? Game { get; set; } = default!;
    [BindProperty]
    public List<Player> Players { get; set; } = default!;
    [BindProperty(SupportsGet = true)]
    public int GameId { get; set; }

    public GameModel(ILogger<GameModel> logger, MaalContext context)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult OnGet()
    {
        Game = _context.Games
            .Find(GameId);
        if (Game == null)
        {
            return RedirectToPage("/NotFound");
        }
        Players = _context.Players
            .Where(x => x.GameId == GameId)
            .ToList();
        return Page();
    }
}