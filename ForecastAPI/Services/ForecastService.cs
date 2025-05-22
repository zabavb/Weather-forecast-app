using ForecastAPI.Models;
using ForecastAPI.Repositories;
using Microsoft.Extensions.Options;
using Refit;

namespace ForecastAPI.Services
{
    public class ForecastService(IForecastRepository repository, IForecastApi forecastApi, IOptions<ForecastApiSettings> forecastApiSettings, ILogger<IForecastService> logger) : IForecastService
    {
        private readonly IForecastRepository _repository = repository;
        private readonly IForecastApi _forecastApi = forecastApi;
        private readonly string _apiKey = forecastApiSettings.Value.ApiKey;
        private readonly ILogger<IForecastService> _logger = logger;
        private string _message = string.Empty;

        public async Task<ForecastResponse> GetForecastAsync(string location, int days)
        {
            if (days < 1 || days > 3)
            {
                _message = "Invalid number of days. Please provide a number between 1 and 3.";
                _logger.LogError(_message);
                throw new Exception(_message);
            }

            var cachedWeather = await _repository.GetForecastFromCacheIfExistsAsync(location, days);

            if (cachedWeather != null)
                return cachedWeather;

            try
            {
                _logger.LogInformation("Fetching from API");
                var response = await _forecastApi.GetForecastAsync(_apiKey, location, days);
                await _repository.SetToCacheAsync(location, days, response);
                
                return response;
            }
            catch (ApiException ex)
            {
                _message = "Weather API error: ";
                _logger.LogError(_message);
                throw new Exception(_message + ex.Message);
            }
            catch (Exception ex)
            {
                _message = "An error occurred while fetching weather data.";
                _logger.LogError(_message);
                throw new Exception(_message, ex);
            }
        }
    }
}
