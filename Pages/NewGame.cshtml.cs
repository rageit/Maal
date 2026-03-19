using Maal.Data;
using Maal.Models;
using Maal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Maal.Pages;

public class NewGameModel : PageModel
{
    private readonly MaalContext _context;
    private readonly IUserIdentificationService _userService;

    [BindProperty]
    public string GameName { get; set; } = "";

    [BindProperty]
    public List<string> PlayerNames { get; set; } = new() { "", "", "" };

    [BindProperty]
    public bool AllowRoundDeletion { get; set; } = true;

    public string? ErrorMessage { get; set; }

    public NewGameModel(MaalContext context, IUserIdentificationService userService)
    {
        _context = context;
        _userService = userService;
    }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        var validNames = PlayerNames
            .Where(n => !string.IsNullOrWhiteSpace(n))
            .Select(n => n.Trim())
            .ToList();

        if (validNames.Count < 2)
        {
            ErrorMessage = "At least 2 players are required.";
            return Page();
        }

        if (validNames.Count > 5)
        {
            ErrorMessage = "Maximum 5 players allowed.";
            return Page();
        }

        if (validNames.Distinct(StringComparer.OrdinalIgnoreCase).Count() != validNames.Count)
        {
            ErrorMessage = "Player names must be unique.";
            return Page();
        }

        var gameName = string.IsNullOrWhiteSpace(GameName)
            ? $"Game {DateTime.Now:MMM dd HH:mm}"
            : GameName.Trim();

        var game = new Game
        {
            Name = gameName,
            TimeStamp = DateTime.UtcNow,
            AllowRoundDeletion = AllowRoundDeletion,
            UserId = _userService.GetUserId(HttpContext)
        };
        _context.Games.Add(game);
        _context.SaveChanges();

        foreach (var name in validNames)
        {
            _context.Players.Add(new Player
            {
                Name = name,
                GameId = game.Id
            });
        }
        _context.SaveChanges();

        return RedirectToPage("/Scoreboard", new { gameId = game.Id });
    }
}
