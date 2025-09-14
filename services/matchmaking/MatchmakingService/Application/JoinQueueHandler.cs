using MatchmakingService.Entities;
using System.Collections.Generic;

namespace MatchmakingService.Application;


public class JoinQueueHandler
{
    private static readonly List<PlayerInQueue> Queue = new();
    private static readonly List<Match> Matches = new();
    private static readonly object _lock = new();

    public string Handle(string playerId, int rating)
    {
        lock (_lock)
        {
            if (Queue.Any(p => p.PlayerId == playerId))
                return "Player already in queue";

            Queue.Add(new PlayerInQueue(playerId, rating));
            TryFormMatch();
            return "Player added to queue";
        }
    }

    private void TryFormMatch()
    {
        if (Queue.Count >= 2)
        {
            var players = Queue.Take(2).ToList();
            foreach (var p in players) Queue.Remove(p);

            var match = new Match(players);
            Matches.Add(match);

            Console.WriteLine($"[EVENT] Match created: {match.MatchId}");
        }
    }
}
