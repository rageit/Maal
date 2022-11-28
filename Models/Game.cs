using System.ComponentModel.DataAnnotations;
namespace Maal.Models;

public class Game
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public List<Round> Rounds { get; set; } = new List<Round>();

    [Display(Name = "Date")]
    [DisplayFormat(DataFormatString = "{0:d}")]
    public DateTime TimeStamp { get; set; }
}