using Microsoft.Extensions.Caching.Memory;

namespace order.flow.utils.cache.memory;

public class LocalCacheService
{
    private readonly IMemoryCache _cache;

    public LocalCacheService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public void Set<T>(string key, T value, int expirationMinutes = 10)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(expirationMinutes)) 
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(expirationMinutes * 2));

        _cache.Set(key, value, cacheEntryOptions);
    }

    public T Get<T>(string key)
    {
        return _cache.TryGetValue(key, out T value) ? value : default;
    }

    public void Remove(string key)
    {
        _cache.Remove(key);
    }
}