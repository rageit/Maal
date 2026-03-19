using Maal.Data;
using Maal.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Maal.Pages;

public class AdminModel : PageModel
{
    private readonly MaalContext _context;

    public List<UserGroup> UserGroups { get; set; } = new();

    public AdminModel(MaalContext context)
    {
        _context = context;
    }

    public void OnGet()
    {
        var games = _context.Games
            .OrderByDescending(g => g.TimeStamp)
            .ToList();

        var grouped = games.GroupBy(g => g.UserId);

        foreach (var group in grouped)
        {
            var userGroup = new UserGroup
            {
                UserId = group.Key,
                Games = new()
            };

            foreach (var game in group)
            {
                var roundCount = _context.Rounds.Count(r => r.GameId == game.Id);
                var playerNames = _context.Players
                    .Where(p => p.GameId == game.Id)
                    .Select(p => p.Name)
                    .ToList();

                userGroup.Games.Add(new GameSummary
                {
                    Game = game,
                    RoundCount = roundCount,
                    PlayerNames = playerNames
                });
            }

            UserGroups.Add(userGroup);
        }
    }

    public class UserGroup
    {
        public string UserId { get; set; } = default!;
        public List<GameSummary> Games { get; set; } = new();
    }

    public class GameSummary
    {
        public Game Game { get; set; } = default!;
        public int RoundCount { get; set; }
        public List<string> PlayerNames { get; set; } = new();
    }
}
