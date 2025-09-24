using Microsoft.Extensions.Caching.Distributed;

namespace XOGameService.API.Repositories
{
    public class RedisXOGameRepository : IXOGameRepository
    {
        private readonly IDistributedCache _distributedCache;

        public RedisXOGameRepository(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache ?? throw new ArgumentNullException(nameof(distributedCache));
        }
    }
}
