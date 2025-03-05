using ForecastAPI.Models;

namespace ForecastAPI.Services
{
    public interface IForecastService
    {
        Task<ForecastResponse> GetForecastAsync(string location, int days);
    }
}
