using Maal.Data;
using Maal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Maal.Pages;

public class GamesModel : PageModel
{
    private readonly MaalContext _context;

    public List<Game> Games { get; set; } = new();
    public Dictionary<int, int> RoundCounts { get; set; } = new();
    public Dictionary<int, List<string>> GamePlayers { get; set; } = new();

    public GamesModel(MaalContext context)
    {
        _context = context;
    }

    public void OnGet()
    {
        Games = _context.Games
            .OrderByDescending(g => g.TimeStamp)
            .ToList();

        foreach (var game in Games)
        {
            RoundCounts[game.Id] = _context.Rounds.Count(r => r.GameId == game.Id);
            GamePlayers[game.Id] = _context.Players
                .Where(p => p.GameId == game.Id)
                .Select(p => p.Name)
                .ToList();
        }
    }

    public IActionResult OnPostDelete(int gameId)
    {
        var game = _context.Games.Find(gameId);
        if (game != null)
        {
            var rounds = _context.Rounds.Where(r => r.GameId == gameId).Include(r => r.RoundPlayers).ToList();
            foreach (var round in rounds)
            {
                _context.RoundPlayers.RemoveRange(round.RoundPlayers);
            }
            _context.Rounds.RemoveRange(rounds);
            _context.Players.RemoveRange(_context.Players.Where(p => p.GameId == gameId));
            _context.Games.Remove(game);
            _context.SaveChanges();
        }
        return RedirectToPage();
    }
}
