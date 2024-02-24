using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }


        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }


        //////// zyade to get all baskets  not important ***
        public async Task<IEnumerable<CustomerBasket>> GetAllBasketsAsync()
        {
            var basketKeys = await GetBasketKeysAsync();
            var baskets = new List<CustomerBasket>();

            foreach (var key in basketKeys)
            {
                var basketData = await _database.StringGetAsync(key);
                if (!basketData.IsNullOrEmpty)
                {
                    var basket = JsonSerializer.Deserialize<CustomerBasket>(basketData);
                    baskets.Add(basket);
                }
            }

            return baskets;
        }

        private async Task<IEnumerable<string>> GetBasketKeysAsync()
        {
            var server = _database.Multiplexer.GetServer(_database.Multiplexer.GetEndPoints().First());
            var keys = server.Keys(pattern: "basket:*");

            return keys.Select(key => (string)key);
        }

        ////////end zyade to get all baskets  not important ***

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            var data = await _database.StringGetAsync(basketId);

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var created = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket),
                TimeSpan.FromDays(30));

            if (!created) return null;

            return await GetBasketAsync(basket.Id);
        }
    }
}
