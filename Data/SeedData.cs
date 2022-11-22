using Maal.Models;
using Maal.Services;
using Microsoft.EntityFrameworkCore;

namespace Maal.Data;
public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MaalContext(
            serviceProvider.GetRequiredService<DbContextOptions<MaalContext>>(),
            serviceProvider.GetRequiredService<IAppSettingsProvider>()))
        {
            if (context == null)
            {
                throw new ArgumentNullException("Null MaalContext");
            }

            // Look for any movies.
            if (context.Players.Any())
            {
                return;   // DB has been seeded
            }

            context.Players.AddRange(
                new Player { Name = "Bhishma" },
                new Player { Name = "Anushree" },
                new Player { Name = "Rejan" }
            );
            context.GameSessions.AddRange(
                new GameSession
                {
                    Name = "Test Game Session",
                    GameSessionTime = DateTime.Now,
                    Games = new List<Game>
                    {
                        new Game
                        {
                            Round = 1,
                            GameTime = DateTime.Now,
                            GamePlayers = new List<GamePlayer>
                            {
                                new GamePlayer
                                {
                                    PlayerId = 1,
                                    Maal = 10,
                                    Dubli = false,
                                    Fouled = false,
                                    Won = true
                                },
                                new GamePlayer
                                {
                                    PlayerId = 2,
                                    Maal = 5,
                                    Dubli = false,
                                    Fouled = false,
                                    Won = false
                                },
                                new GamePlayer
                                {
                                    PlayerId = 3,
                                    Maal = 0,
                                    Dubli = false,
                                    Fouled = true,
                                    Won = false
                                }
                            }
                        }
                    }
                },
                new GameSession
                {
                    Name = "Test Game Session 2",
                    GameSessionTime = DateTime.Now,
                    Games = new List<Game>
                    {
                        new Game
                        {
                            Round = 2,
                            GameTime = DateTime.Now,
                            GamePlayers = new List<GamePlayer>
                            {
                                new GamePlayer
                                {
                                    PlayerId = 1,
                                    Maal = 30,
                                    Dubli = false,
                                    Fouled = false,
                                    Won = true
                                },
                                new GamePlayer
                                {
                                    PlayerId = 2,
                                    Maal = 17,
                                    Dubli = true,
                                    Fouled = false,
                                    Won = false
                                }
                            }
                        }
                    }
                }
            );
            context.SaveChanges();
        }
    }
}