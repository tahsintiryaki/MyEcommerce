namespace MyEcommerce.Cache;

public interface ICacheService
{
    Task SetAsync(string cacheKey, string response, TimeSpan expireTimeSeconds);
    Task SetAsync(string cacheKey, object response, TimeSpan expireTimeSeconds);
    Task SetAsync(string cacheKey, object response);
    Task<T> GetAsync<T>(string cacheKey) where T : class;
    IAsyncEnumerable<string> GetKeysAsync(string pattern);
    Task RemoveAsync(string cacheKey);
    Task<bool> IfExistAsync(List<string> Ids);
}