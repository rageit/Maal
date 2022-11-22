using System.ComponentModel.DataAnnotations;

namespace Maal.Models;

public class Player
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}
