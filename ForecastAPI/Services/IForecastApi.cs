using ForecastAPI.Models;
using Refit;

namespace ForecastAPI.Services
{
    public interface IForecastApi
    {
        [Get("/v1/forecast.json?key={apiKey}&q={location}&days={days}&aqi=no&alerts=no")]
        Task<ForecastResponse> GetForecastAsync(string apiKey, string location, int days);
    }
}
