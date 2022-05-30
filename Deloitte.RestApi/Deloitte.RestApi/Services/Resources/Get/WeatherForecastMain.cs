using Newtonsoft.Json;

namespace Deloitte.RestApi.Services.Resources.Get
{
    public class WeatherForecastMain
    {
        [JsonProperty("feels_like")] public decimal FeelsLike { get; set; }

        [JsonProperty("temp")] public decimal Temperature { get; set; }

        [JsonProperty("temp_max")] public decimal TemperatureMax { get; set; }

        [JsonProperty("temp_min")] public decimal TemperatureMin { get; set; }
    }
}