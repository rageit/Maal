using System.ComponentModel.DataAnnotations;
namespace Maal.Models;

public class GameSession
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public List<Game> Games { get; set; } = new List<Game>();
    public DateTime GameSessionTime { get; set; }
}