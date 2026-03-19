using Maal.Data;
using Maal.Models;
using Maal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Maal.Pages;

public class ScoreboardModel : PageModel
{
    private readonly MaalContext _context;
    private readonly IUserIdentificationService _userService;

    [BindProperty(SupportsGet = true)]
    public int GameId { get; set; }

    public Game? Game { get; set; }
    public List<Player> Players { get; set; } = new();
    public List<Round> Rounds { get; set; } = new();
    public Dictionary<int, int> PlayerTotals { get; set; } = new();

    public ScoreboardModel(MaalContext context, IUserIdentificationService userService)
    {
        _context = context;
        _userService = userService;
    }

    public IActionResult OnGet()
    {
        var userId = _userService.GetUserId(HttpContext);
        Game = _context.Games.Find(GameId);
        if (Game == null || Game.UserId != userId)
            return RedirectToPage("/Index");

        Players = _context.Players
            .Where(p => p.GameId == GameId)
            .OrderBy(p => p.Id)
            .ToList();

        Rounds = _context.Rounds
            .Where(r => r.GameId == GameId)
            .Include(r => r.RoundPlayers)
            .OrderBy(r => r.Number)
            .ToList();

        foreach (var player in Players)
        {
            PlayerTotals[player.Id] = Rounds
                .SelectMany(r => r.RoundPlayers)
                .Where(rp => rp.PlayerId == player.Id)
                .Sum(rp => rp.Points);
        }

        return Page();
    }

    public IActionResult OnPostDeleteRound(int roundId)
    {
        var userId = _userService.GetUserId(HttpContext);
        var game = _context.Games.Find(GameId);
        if (game == null || game.UserId != userId || !game.AllowRoundDeletion)
            return RedirectToPage(new { gameId = GameId });

        var round = _context.Rounds
            .Include(r => r.RoundPlayers)
            .FirstOrDefault(r => r.Id == roundId && r.GameId == GameId);

        if (round != null)
        {
            _context.RoundPlayers.RemoveRange(round.RoundPlayers);
            _context.Rounds.Remove(round);
            _context.SaveChanges();

            // Re-number remaining rounds
            var remaining = _context.Rounds
                .Where(r => r.GameId == GameId)
                .OrderBy(r => r.Number)
                .ToList();
            for (int i = 0; i < remaining.Count; i++)
            {
                remaining[i].Number = i + 1;
            }
            _context.SaveChanges();
        }

        return RedirectToPage(new { gameId = GameId });
    }
}
