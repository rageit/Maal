using Maal.Models;
using Maal.Services;
using Microsoft.EntityFrameworkCore;
namespace Maal.Data;
public class MaalContext : DbContext
{
    public string DbPath { get; }
    public DbSet<Game> Games { get; set; } = default!;
    public DbSet<Round> Rounds { get; set; } = default!;
    public DbSet<RoundPlayer> RoundPlayers { get; set; } = default!;
    public DbSet<Player> Players { get; set; } = default!;

    public MaalContext(DbContextOptions<MaalContext> options, IAppSettingsProvider appSettingsProvider)
        : base(options)
    {
        DbPath = appSettingsProvider.DbPath;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}