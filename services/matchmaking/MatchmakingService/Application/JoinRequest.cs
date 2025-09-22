namespace MatchmakingService.Application
{
    public class JoinRequest
    {
        public required string PlayerId { get; set; }
        public int Rating { get; set; }
    }
}
