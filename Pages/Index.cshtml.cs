using Maal.Data;
using Maal.Models;
using Maal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Maal.Pages;

public class IndexModel : PageModel
{
    private readonly MaalContext _context;
    private readonly IUserIdentificationService _userService;

    public Game? CurrentGame { get; set; }
    public List<Player> Players { get; set; } = new();
    public int TotalRounds { get; set; }
    public Dictionary<int, int> PlayerTotals { get; set; } = new();

    public IndexModel(MaalContext context, IUserIdentificationService userService)
    {
        _context = context;
        _userService = userService;
    }

    public void OnGet()
    {
        var userId = _userService.GetUserId(HttpContext);

        CurrentGame = _context.Games
            .Where(g => g.UserId == userId)
            .OrderByDescending(g => g.TimeStamp)
            .FirstOrDefault();

        if (CurrentGame != null)
        {
            Players = _context.Players
                .Where(p => p.GameId == CurrentGame.Id)
                .ToList();

            TotalRounds = _context.Rounds
                .Count(r => r.GameId == CurrentGame.Id);

            var roundPlayers = _context.Rounds
                .Where(r => r.GameId == CurrentGame.Id)
                .SelectMany(r => r.RoundPlayers)
                .ToList();

            foreach (var player in Players)
            {
                PlayerTotals[player.Id] = roundPlayers
                    .Where(rp => rp.PlayerId == player.Id)
                    .Sum(rp => rp.Points);
            }
        }
    }
}
