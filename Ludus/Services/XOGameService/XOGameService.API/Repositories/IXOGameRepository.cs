using XOGameService.API.Models;

namespace XOGameService.API.Repositories
{
    public interface IXOGameRepository
    {
        Task<GameState?> GetAsync(string gameId, CancellationToken ct = default);
        Task CreateAsync(GameState gameState, CancellationToken ct = default);
        Task<bool> TryUpdateAsync(GameState newState, int expectedVersion,  CancellationToken ct = default);
    }
}
