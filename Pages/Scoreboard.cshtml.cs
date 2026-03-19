using Maal.Data;
using Maal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Maal.Pages;

public class ScoreboardModel : PageModel
{
    private readonly MaalContext _context;

    [BindProperty(SupportsGet = true)]
    public int GameId { get; set; }

    public Game? Game { get; set; }
    public List<Player> Players { get; set; } = new();
    public List<Round> Rounds { get; set; } = new();
    public Dictionary<int, int> PlayerTotals { get; set; } = new();

    public ScoreboardModel(MaalContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        Game = _context.Games.Find(GameId);
        if (Game == null)
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
        var game = _context.Games.Find(GameId);
        if (game == null || !game.AllowRoundDeletion)
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
