using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Maal.Data;
using Maal.Models;
using Microsoft.EntityFrameworkCore;

namespace Maal.Pages;

public class RoundsModel : PageModel
{
    private readonly ILogger<RoundsModel> _logger;
    private MaalContext _context;

    [BindProperty]
    public List<Round> Rounds { get; set; } = default!;
    [BindProperty]
    public List<Player> Players { get; set; } = default!;
    [BindProperty(SupportsGet = true)]
    public int GameId { get; set; }

    public RoundsModel(ILogger<RoundsModel> logger, MaalContext context)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult OnGet()
    {
        var game = _context.Games
            .Include("Rounds.RoundPlayers.Player")
            .FirstOrDefault(x => x.Id == GameId);
        if (game == null)
        {
            return RedirectToPage("/NotFound");
        }
        Rounds = game.Rounds.ToList();
        Players = game.Rounds
            .SelectMany(x => x.RoundPlayers)
            .Select(x => x.Player)
            .Distinct()
            .ToList();
        return Page();
    }
}