
using MatchmakingService.Entities; 
namespace MatchmakingService.Entities;

public class Match
{
    public Guid MatchId { get; private set; } = Guid.NewGuid();
    public List<PlayerInQueue> Players { get; private set; }
    public string Status { get; private set; } = "ready";

    public Match(IEnumerable<PlayerInQueue> players)
    {
        Players = new List<PlayerInQueue>(players);
    }
}
