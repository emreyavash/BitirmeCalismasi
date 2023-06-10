using ETicaret.Baskets.Entities;
using ETicaret.Baskets.Extensions;
using ETicaret.Baskets.Repositories.Interface;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Text.Json;

namespace ETicaret.Baskets.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ConnectionMultiplexer _redis;
        //private readonly IRedisRegistration _redis;
        private readonly IDatabase _database;

        public BasketRepository(ConnectionMultiplexer redis)
        {
            _redis = redis;
            _database = _redis.GetDatabase(0);
        }

        public async Task<bool> DeleteBasket(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

       

        public async Task<UserBasket> GetBasketByUserId(string userId)
        {
            var userBasket = await _database.StringGetAsync(userId);
            if (userBasket.IsNullOrEmpty)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<UserBasket>(userBasket);
        }
        //public async Task<IEnumerable<Basket>> GetBasketsByUserId(string UserId)
        //{
        //    var userBasket = await _database.StringGetAsync(UserId);

        //    if (userBasket.IsNullOrEmpty)
        //    {
        //        return null;
        //    }
        //    return JsonConvert.DeserializeObject<IEnumerable<Basket>>(userBasket);
        //}


        public async Task<bool> SetBasket(UserBasket basket)
        {
            var created = await _database.StringSetAsync(basket.UserId, JsonConvert.SerializeObject(basket));
          
            return created;
        }
    }
}
