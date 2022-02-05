namespace Services.Interfaces;

public interface IRedisCacheAccessor
{
    Task<T> GetCacheValueAsync<T>(string[] keys);

    Task SetCacheValueAsync<T>(string[] keys, T value);
    Task KeyDeleteDirectAsync(string key);
}
