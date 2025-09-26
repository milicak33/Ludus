using XOGameService.API.Models;
using XOGameService.API.Repositories;

namespace XOGameService.API.Services
{
    public class XOGameService : IXOGameService
    {
        private readonly IXOGameRepository _repository;

        public XOGameService(IXOGameRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<GameState> CreateGame(string playerXId, string playerOId)
        {
            var gameState = new GameState
            {
                PlayerXId = playerXId,
                PlayerOId = playerOId
            };

            await _repository.CreateAsync(gameState);
            return gameState;
        }

        public async Task<GameState?> GetGame(string gameId)
        {
            return await _repository.GetAsync(gameId);
        }

        public Task<GameState> MakeMove(string gameId, int CellIndex, int expectedVersion, string actingUserId)
        {
            throw new NotImplementedException();
        }
    }
}
