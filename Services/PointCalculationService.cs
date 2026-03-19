using Maal.Models;

namespace Maal.Services;

public static class PointCalculationService
{
    /// <summary>
    /// Calculates and assigns points for all players in a round based on Marriage card game rules.
    ///
    /// Scoring rules:
    /// - Winner gets +3 from each SEEN opponent
    /// - Winner gets +10 from each UNSEEN opponent
    /// - Winner gets +5 from each opponent if won with 8 dublees
    /// - Between winner and each SEEN opponent, maal difference is settled
    /// - Foul player pays 10 to each other player
    /// </summary>
    public static void CalculatePoints(Round round, List<RoundPlayer> roundPlayers)
    {
        var activePlayers = roundPlayers.Where(rp => !rp.SkippedRound).ToList();
        var winner = activePlayers.FirstOrDefault(rp => rp.PlayerId == round.WinnerId);

        if (winner == null || activePlayers.Count < 2)
            return;

        bool isDubleeWin = winner.Dubli;
        int winnerMaal = winner.Maal;

        // Initialize all points to 0
        foreach (var rp in roundPlayers)
            rp.Points = 0;

        foreach (var opponent in activePlayers.Where(rp => rp.PlayerId != round.WinnerId))
        {
            int payment;

            if (opponent.Seen)
            {
                // Seen opponent pays: 3 (seen fee) + maal difference
                int maalDiff = winnerMaal - opponent.Maal;
                payment = 3 + maalDiff;
            }
            else
            {
                // Unseen opponent pays flat 10
                payment = 10;
            }

            // Dublee bonus: winner gets extra 5 from each opponent
            if (isDubleeWin)
                payment += 5;

            opponent.Points -= payment;
            winner.Points += payment;
        }

        // Foul: foul player pays 10 to each other active player
        if (round.FoulPlayerId.HasValue)
        {
            var foulPlayer = activePlayers.FirstOrDefault(rp => rp.PlayerId == round.FoulPlayerId.Value);
            if (foulPlayer != null)
            {
                var otherPlayers = activePlayers.Where(rp => rp.PlayerId != foulPlayer.PlayerId).ToList();
                foulPlayer.Points -= 10 * otherPlayers.Count;
                foreach (var other in otherPlayers)
                {
                    other.Points += 10;
                }
            }
        }
    }
}
