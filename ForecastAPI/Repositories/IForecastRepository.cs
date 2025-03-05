using ForecastAPI.Models;

namespace ForecastAPI.Repositories
{
    public interface IForecastRepository
    {
        Task<ForecastResponse?> GetForecastFromCacheIfExistsAsync(string location, int days);
        Task SetToCacheAsync(string location, int days, ForecastResponse weather);
    }
}
