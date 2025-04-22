using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace MyEcommerce.Cache;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    private readonly ILogger<CacheService> _logger;

    public CacheService(IDistributedCache distributedCache, IConnectionMultiplexer connectionMultiplexer,
        ILogger<CacheService> logger)
    {
        _distributedCache = distributedCache;
        _connectionMultiplexer = connectionMultiplexer;
        _logger = logger;
    }

    public async Task SetAsync(string cacheKey, string response, TimeSpan expireTimeSeconds)
    {
        try
        {
            if (string.IsNullOrEmpty(response))
                return;

            await _distributedCache.SetStringAsync(cacheKey, response, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expireTimeSeconds
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error on Saving Redis");
        }
    }

    public async Task SetAsync(string cacheKey, object response, TimeSpan expireTimeSeconds)
    {
        try
        {
            if (response == null)
                return;

            var serializedResponse = JsonConvert.SerializeObject(response);

            await _distributedCache.SetStringAsync(cacheKey, serializedResponse, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expireTimeSeconds
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error on Saving Redis");
        }
    }

    public Task SetAsync(string cacheKey, object response)
    {
        return SetAsync(cacheKey, response, TimeSpan.FromHours(1));
    }

    public async Task<T> GetAsync<T>(string cacheKey) where T : class
    {
        try
        {
            var cachedResponse = await _distributedCache.GetStringAsync(cacheKey);
            if (string.IsNullOrEmpty(cachedResponse))
                return null;

            var deserializeObject = JsonConvert.DeserializeObject<T>(cachedResponse);
            return deserializeObject;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error on Saving Redis");
            return null;
        }
    }

    public async IAsyncEnumerable<string> GetKeysAsync(string pattern)
    {
        if (string.IsNullOrWhiteSpace(pattern))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(pattern));

        foreach (var endpoint in _connectionMultiplexer.GetEndPoints())
        {
            var server = _connectionMultiplexer.GetServer(endpoint);
            await foreach (var key in server.KeysAsync(pattern: pattern))
            {
                yield return key.ToString();
            }
        }
    }

    public async Task RemoveAsync(string cacheKey)
    {
        await _distributedCache.RemoveAsync(cacheKey);
    }

    public async Task<bool> IfExistAsync(List<string> Ids)
    {
        var cachedProducts = GetKeysAsync("ProductList-*");
        return true;
    }
}