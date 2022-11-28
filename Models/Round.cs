using System.ComponentModel.DataAnnotations;
namespace Maal.Models;

public class Round
{
    public int Id { get; set; }
    public List<RoundPlayer> RoundPlayers { get; set; } = new List<RoundPlayer>();
    public int Number { get; set; }
    public int WinnerId { get; set; }
    public Player Winner { get;set; } = default!;
    public int? FoulPlayerId { get; set; }
    public Player FoulPlayer { get;set; } = default!;
    public DateTime TimeStamp { get; set; }
    public int GameId { get; set; }
    public Game Game { get; set; } = default!;
}