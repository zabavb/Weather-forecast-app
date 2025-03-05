using ForecastAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForecastAPI.Controllers
{
    /// <summary>
    /// Controller for retrieving data from weather API https://api.weatherapi.com.
    /// </summary>
    /// <remarks>
    /// This controller provides endpoint for retrieving data about weather forecasts
    /// </remarks>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ForecastsController"/> class.
    /// </remarks>
    /// <param name="forecastService">Service for fetching weather forecasts.</param>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ForecastsController(IForecastService forecastService) : ControllerBase
    {
        private readonly IForecastService _forecastService = forecastService;

        /// <summary>
        /// Retrieves a weather forecasts by their location and count of days in advance.
        /// </summary>
        /// <param name="location">The name of the city, locality or region.</param>
        /// <param name="days">The count of days to fetch (maximum 14 days).</param>
        /// <returns>The user with the specified ID.</returns>
        /// <response code="200">Returns the weather forecast if all parameters are well specified.</response>
        /// <response code="400">If specified location does not found or
        /// provided count of days are beyond limits.</response>
        [HttpGet("{location}/{days}")]
        public async Task<IActionResult> GetForecast(string location, int days)
        {
            try
            {
                var weatherData = await _forecastService.GetForecastAsync(location, days);
                return Ok(weatherData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
