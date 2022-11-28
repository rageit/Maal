using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Maal.Models;

public class RoundPlayer
{
    public int Id { get; set; }
    public bool SkippedRound{ get; set; }
    public bool Seen { get; set; }
    public int Maal { get; set; }
    public bool Dubli { get; set; }
    public int Points { get; set; }
    public int PlayerId { get; set; }
    public Player Player { get; set; } = default!;
    public int RoundId { get; set; }
    public Round Round { get; set; } = default!;
}
