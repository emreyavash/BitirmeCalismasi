using StackExchange.Redis;

namespace ETicaret.Baskets.Extensions
{
    public static class RedisRegister
    {
        public static ConnectionMultiplexer ConfigureRedis(this IServiceProvider services,IConfiguration configuration)
        {
            var redisConf = ConfigurationOptions.Parse(configuration["RedisSettings:ConnectionString"]);
            redisConf.ResolveDns = true;
            redisConf.AbortOnConnectFail = false;
            redisConf.AllowAdmin = true;
            return ConnectionMultiplexer.Connect(redisConf);
        }
    }
}
