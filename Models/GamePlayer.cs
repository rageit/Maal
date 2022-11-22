using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Maal.Models;

[PrimaryKey(nameof(GameId), nameof(PlayerId))]
public class GamePlayer
{
    public int GameId { get; set; }
    public int PlayerId { get; set; }
    public int Maal { get; set; }
    public bool Dubli { get; set; }
    public bool Won { get; set; }
    public bool Fouled { get; set; }
    public Game Game { get; set; } = default!;
    public Player Player { get; set; } = default!;
}
