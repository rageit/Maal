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
            if (context.Players.Any())
            {
                return;   // DB has been seeded
            }

            var game1 = new Game
            {
                Id = 100,
                Name = "Test Game Session 1",
                TimeStamp = DateTime.UtcNow
            };
            var bhishma1 = new Player { Id = 1, GameId = 100,  Name = "Bhishma" };
            var anushree1 = new Player { Id = 2, GameId = 100, Name = "Anushree" };
            var rejan1 = new Player { Id = 3, GameId = 100, Name = "Rejan" };
            var game1Round1 = new Round
            {
                Id = 1,
                Number = 1,
                TimeStamp = DateTime.UtcNow,
                WinnerId = bhishma1.Id,
                
                GameId = game1.Id,
            };
            var game1Round2 = new Round
            {
                Id = 2,
                Number = 2,
                TimeStamp = DateTime.UtcNow,
                WinnerId = anushree1.Id,
                
                GameId = game1.Id,
            };
            var game1Round1Player1 = new RoundPlayer
            {
                Id = 1,
                RoundId = game1Round1.Id,
                PlayerId = bhishma1.Id,
                Maal = 10,
                Dubli = false,
                Seen = true,
            };
            var game1Round1Player2 = new RoundPlayer
            {
                Id = 2,
                RoundId = game1Round1.Id,
                PlayerId = anushree1.Id,
                Maal = 5,
                Dubli = false,
                Seen = true,
            };
            var game1Round1Player3 = new RoundPlayer
            {
                Id = 3,
                RoundId = game1Round1.Id,
                PlayerId = rejan1.Id,
                Maal = 0,
                Dubli = false,
                Seen = false,
            };
            var game1Round2Player1 = new RoundPlayer
            {
                Id = 4,
                RoundId = game1Round2.Id,
                PlayerId = bhishma1.Id,
                Maal = 0,
                Dubli = false,
                Seen = true,
            };
            var game1Round2Player2 = new RoundPlayer
            {
                Id = 5,
                RoundId = game1Round2.Id,
                PlayerId = anushree1.Id,
                Maal = 10,
                Dubli = true,
                Seen = true,
            };
            var game1Round2Player3 = new RoundPlayer
            {
                Id = 6,
                RoundId = game1Round2.Id,
                PlayerId = rejan1.Id,
                Maal = 5,
                Dubli = false,
                Seen = true,
            };

            var game2 = new Game
            {
                Id = 101,
                Name = "Test Game Session 2",
                TimeStamp = DateTime.UtcNow
            };
            var bhishma2 = new Player { Id = 4, GameId = 101,  Name = "Bhishma" };
            var anushree2 = new Player { Id = 5, GameId = 101, Name = "Anushree" };
            var rejan2 = new Player { Id = 6, GameId = 101, Name = "Rejan" };
            var game2Round1 = new Round
            {
                Id = 3,
                Number = 1,
                TimeStamp = DateTime.UtcNow,
                WinnerId = bhishma2.Id,
                GameId = game2.Id,
            };
            var game2Round1Player1 = new RoundPlayer
            {
                Id = 7,
                RoundId = game2Round1.Id,
                PlayerId = bhishma2.Id,
                Maal = 30,
                Dubli = false,
                Seen = true,
            };
            var game2Round1Player2 = new RoundPlayer
            {
                Id = 8,
                RoundId = game2Round1.Id,
                PlayerId = anushree2.Id,
                Maal = 17,
                Dubli = true,
                Seen = true,
            };
            var game2Round2 = new Round
            {
                Id = 4,
                Number = 2,
                TimeStamp = DateTime.UtcNow,
                WinnerId = bhishma2.Id,
                FoulPlayerId = rejan2.Id,
                GameId = game2.Id,
            };
            var game2Round2Player1 = new RoundPlayer
            {
                Id = 9,
                RoundId = game2Round2.Id,
                PlayerId = bhishma2.Id,
                Maal = 5,
                Dubli = false,
                Seen = true,
            };
            var game2Round2Player2 = new RoundPlayer
            {
                Id = 10,
                RoundId = game2Round2.Id,
                PlayerId = rejan2.Id,
                Maal = 15,
                Dubli = true,
                Seen = true,
            };
            context.Games.AddRange(game1, game2);
            context.Players.AddRange(bhishma1, anushree1, rejan1, bhishma2, anushree2, rejan2);
            context.Rounds.AddRange(game1Round1, game1Round2, game2Round1, game2Round2);
            context.RoundPlayers.AddRange(
                game1Round1Player1, game1Round1Player2, game1Round1Player3,
                game1Round2Player1, game1Round2Player2, game1Round2Player3,
                game2Round1Player1, game2Round1Player2,
                game2Round2Player1, game2Round2Player2);

            context.SaveChanges();
        }
    }
}