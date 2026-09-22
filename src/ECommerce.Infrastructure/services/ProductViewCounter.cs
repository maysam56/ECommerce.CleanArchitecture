
using ECommerce.Application.Interfaces.IServices;
using StackExchange.Redis;

namespace ECommerce.Infrastructure.services
{
    public class ProductViewCounter : IProductViewCounter
    {
        private readonly IConnectionMultiplexer _redis;

        public ProductViewCounter(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public async Task IncrementAsync(
            int productId,
            CancellationToken cancellationToken = default)
        {
            var db = _redis.GetDatabase();

            var key = $"product:views:{productId}";

            await db.StringIncrementAsync(key);

            await db.SetAddAsync("product:viewed-products", productId);
      
        }
        public async Task<IEnumerable<int>> GetViewedProductIdsAsync(
           CancellationToken cancellationToken = default)
        {
            var db = _redis.GetDatabase();

            var values = await db.SetMembersAsync(
                "product:viewed-products");

            return values
                .Where(value => value.HasValue)
                .Select(value => (int)value);
        }
     
        public async Task<long> TakeCountAsync(
            int productId,
            CancellationToken cancellationToken = default)
        {
            var db = _redis.GetDatabase();

            var key = $"product:views:{productId}";

            const string script = """
                            local value = redis.call('GET', KEYS[1])

                            if not value then
                                return 0
                            end

                            redis.call('DEL', KEYS[1])

                            return value
                            """;

            var result = await db.ScriptEvaluateAsync(script,  new RedisKey[] { key });
    
            return (long)result;
        }
        public async Task RemoveViewedProductAsync(
             int productId,
             CancellationToken cancellationToken = default)
        {
            var db = _redis.GetDatabase();

            await db.SetRemoveAsync( "product:viewed-products", productId);
               
               
        }
    }
}