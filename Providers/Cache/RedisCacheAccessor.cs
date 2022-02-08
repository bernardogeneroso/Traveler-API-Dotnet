using System.Collections;
using System.Text.Json;
using Services.Interfaces;
using StackExchange.Redis;

namespace Providers.Cache;

public class RedisCacheAccessor : IRedisCacheAccessor
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    private readonly IOriginAccessor _originAccessor;
    public RedisCacheAccessor(IConnectionMultiplexer connectionMultiplexer, IOriginAccessor originAccessor)
    {
        _originAccessor = originAccessor;
        _connectionMultiplexer = connectionMultiplexer;
    }

    public async Task<T> GetCacheValueAsync<T>(params string[] keys)
    {
        var keyMaster = CreateKeyMaster(keys);

        var db = _connectionMultiplexer.GetDatabase();
        var value = await db.StringGetAsync(keyMaster);

        if (value.IsNullOrEmpty || !value.HasValue) return default;

        var serializedValue = JsonSerializer.Deserialize<T>(value);

        if (serializedValue is IList list && list.Count == 0) return default;

        Console.WriteLine($"Cache => {keyMaster} | Time: {DateTime.Now.GetDateTimeFormats()}");

        return JsonSerializer.Deserialize<T>(value);
    }

    public async Task SetCacheValueAsync<T>(T value, params string[] keys)
    {
        var keyMaster = CreateKeyMaster(keys);

        var timeSpan = new TimeSpan(0, 0, 10, 0);

        var serializedValue = JsonSerializer.Serialize(value);

        var db = _connectionMultiplexer.GetDatabase();
        await db.StringSetAsync(keyMaster, serializedValue, timeSpan);
    }

    public async Task KeyDeleteDirectAsync(string key)
    {
        var db = _connectionMultiplexer.GetDatabase();
        await db.KeyDeleteAsync(key);
    }

    private string CreateKeyMaster(params string[] keys)
    {
        var keyMaster = _originAccessor.GetRoutePath();

        foreach (var key in keys)
        {
            keyMaster += $"_{key}";
        }

        return keyMaster;
    }
}
