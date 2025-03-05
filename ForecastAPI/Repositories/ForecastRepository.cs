using ForecastAPI.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace ForecastAPI.Repositories
{
    public class ForecastRepository(IConnectionMultiplexer redis, ILogger<IForecastRepository> logger) : IForecastRepository
    {
        private readonly IDatabase _redisDatabase = redis.GetDatabase();
        public readonly string _cacheKeyPrefix = "Weather_";
        public readonly TimeSpan _cacheExpiration = TimeSpan.FromMinutes(10);
        private readonly ILogger<IForecastRepository> _logger = logger;

        public async Task<ForecastResponse?> GetForecastFromCacheIfExistsAsync(string location, int days)
        {
            var cachedWeather = await _redisDatabase.HashGetAsync(_cacheKeyPrefix + Identifier(location, days), "data");
            if (!cachedWeather.IsNullOrEmpty)
            {
                _logger.LogInformation("Fetched from CACHE.");
                return JsonSerializer.Deserialize<ForecastResponse>(cachedWeather!);
            }

            return null;
        }

        public async Task SetToCacheAsync(string location, int days, ForecastResponse weather)
        {
            if (weather != null)
            {
                _logger.LogInformation("Set to CACHE.");
                await _redisDatabase.HashSetAsync(
                    _cacheKeyPrefix + Identifier(location, days),
                    "data",
                    JsonSerializer.Serialize(weather)
                );

                await _redisDatabase.KeyExpireAsync(_cacheKeyPrefix + Identifier(location, days), _cacheExpiration);
            }
        }

        private static string Identifier(string location, int days) => $"{location}_{days}";
    }
}
