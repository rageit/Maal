using System.ComponentModel.DataAnnotations;
namespace Maal.Models;

public class Game
{
    public int Id { get; set; }
    public List<GamePlayer> GamePlayers { get; set; } = new List<GamePlayer>();
    public int Round { get; set; }
    public DateTime GameTime { get; set; }

}
