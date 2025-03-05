using System.Text.Json.Serialization;

namespace ForecastAPI.Models
{
    public class ForecastResponse
    {
        [JsonPropertyName("location")]
        public LocationInfo? Location { get; set; }

        [JsonPropertyName("current")]
        public CurrentWeather? Current { get; set; }

        [JsonPropertyName("forecast")]
        public Forecast? Forecast { get; set; }
    }

    public class LocationInfo
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("region")]
        public string? Region { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }
    }

    public class CurrentWeather
    {
        [JsonPropertyName("temp_c")]
        public double TemperatureCelsius { get; set; }

        [JsonPropertyName("temp_f")]
        public double TemperatureFahrenheit { get; set; }

        [JsonPropertyName("condition")]
        public WeatherCondition? Condition { get; set; }

        [JsonPropertyName("wind_kph")]
        public double WindSpeedKph { get; set; }

        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }
    }

    public class WeatherCondition
    {
        [JsonPropertyName("text")]
        public string? Description { get; set; }

        [JsonPropertyName("icon")]
        public string? IconUrl { get; set; }
    }

    public class Forecast
    {
        [JsonPropertyName("forecastday")]
        public List<ForecastDay>? ForecastDays { get; set; }
    }

    public class ForecastDay
    {
        [JsonPropertyName("date")]
        public string? Date { get; set; }

        [JsonPropertyName("day")]
        public DailyWeather? Day { get; set; }
    }

    public class DailyWeather
    {
        [JsonPropertyName("maxtemp_c")]
        public double MaxTempCelsius { get; set; }

        [JsonPropertyName("mintemp_c")]
        public double MinTempCelsius { get; set; }

        [JsonPropertyName("avgtemp_c")]
        public double AvgTempCelsius { get; set; }

        [JsonPropertyName("condition")]
        public WeatherCondition? Condition { get; set; }
    }
}
