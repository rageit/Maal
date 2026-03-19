using Maal.Data;
using Maal.Models;
using Maal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Maal.Pages;

public class PlayModel : PageModel
{
    private readonly MaalContext _context;

    [BindProperty(SupportsGet = true)]
    public int GameId { get; set; }

    public Game? Game { get; set; }
    public List<Player> Players { get; set; } = new();
    public int NextRoundNumber { get; set; }

    [BindProperty]
    public int WinnerId { get; set; }

    [BindProperty]
    public int? FoulPlayerId { get; set; }

    [BindProperty]
    public Dictionary<int, int> MaalValues { get; set; } = new();

    [BindProperty]
    public Dictionary<int, bool> SeenValues { get; set; } = new();

    [BindProperty]
    public Dictionary<int, bool> SkippedValues { get; set; } = new();

    [BindProperty]
    public bool DubleeWin { get; set; }

    public string? ErrorMessage { get; set; }

    public PlayModel(MaalContext context)
    {
        _context = context;
    }

    public IActionResult OnGet()
    {
        return LoadPage();
    }

    public IActionResult OnPost()
    {
        var result = LoadPage();
        if (Game == null) return result;

        // Validate winner
        if (WinnerId == 0)
        {
            ErrorMessage = "Please select a winner.";
            return Page();
        }

        var activePlayerIds = Players
            .Where(p => !SkippedValues.GetValueOrDefault(p.Id, false))
            .Select(p => p.Id)
            .ToList();

        if (!activePlayerIds.Contains(WinnerId))
        {
            ErrorMessage = "Winner cannot be a skipped player.";
            return Page();
        }

        if (FoulPlayerId.HasValue && FoulPlayerId.Value == 0)
            FoulPlayerId = null;

        // Create round
        var round = new Round
        {
            Number = NextRoundNumber,
            WinnerId = WinnerId,
            FoulPlayerId = FoulPlayerId,
            TimeStamp = DateTime.UtcNow,
            GameId = GameId
        };
        _context.Rounds.Add(round);
        _context.SaveChanges();

        // Create round players
        var roundPlayers = new List<RoundPlayer>();
        foreach (var player in Players)
        {
            bool skipped = SkippedValues.GetValueOrDefault(player.Id, false);
            bool seen = SeenValues.GetValueOrDefault(player.Id, false);
            int maal = MaalValues.GetValueOrDefault(player.Id, 0);

            var rp = new RoundPlayer
            {
                RoundId = round.Id,
                PlayerId = player.Id,
                SkippedRound = skipped,
                Seen = skipped ? false : seen,
                Maal = skipped ? 0 : maal,
                Dubli = player.Id == WinnerId && DubleeWin,
                Points = 0
            };
            roundPlayers.Add(rp);
        }

        // Calculate points
        PointCalculationService.CalculatePoints(round, roundPlayers);

        _context.RoundPlayers.AddRange(roundPlayers);
        _context.SaveChanges();

        return RedirectToPage("/Scoreboard", new { gameId = GameId });
    }

    private IActionResult LoadPage()
    {
        Game = _context.Games.Find(GameId);
        if (Game == null)
            return RedirectToPage("/Index");

        Players = _context.Players
            .Where(p => p.GameId == GameId)
            .OrderBy(p => p.Id)
            .ToList();

        NextRoundNumber = _context.Rounds
            .Where(r => r.GameId == GameId)
            .Select(r => r.Number)
            .DefaultIfEmpty(0)
            .Max() + 1;

        return Page();
    }
}
