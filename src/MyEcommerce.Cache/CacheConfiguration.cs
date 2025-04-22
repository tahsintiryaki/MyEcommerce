using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace MyEcommerce.Cache;

public static class CacheConfiguration
{
    public static void CacheConfigurator(this IServiceCollection services, IConfiguration configuration)
    {
        var redisCacheOptions = configuration.GetSection("RedisCache").Get<RedisCacheOptions>();
        services.AddSingleton(redisCacheOptions);

        services.AddStackExchangeRedisCache(
            option => { option.Configuration = redisCacheOptions.ConnectionString; });
        services.AddSingleton<IConnectionMultiplexer>(
            ConnectionMultiplexer.Connect(redisCacheOptions.ConnectionString));

        services.AddScoped<ICacheService, CacheService>();
    }
}