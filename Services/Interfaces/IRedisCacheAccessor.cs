namespace Services.Interfaces;

public interface IRedisCacheAccessor
{
    Task<T> GetCacheValueAsync<T>(params string[] keys);

    Task SetCacheValueAsync<T>(T value, params string[] keys);
    Task KeyDeleteDirectAsync(string key);
}
