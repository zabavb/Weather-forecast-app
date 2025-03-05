namespace ForecastAPI.Models
{
    public class ForecastApiSettings
    {
        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }

        public ForecastApiSettings()
        {
            BaseUrl = string.Empty;
            ApiKey = string.Empty;
        }
    }
}
