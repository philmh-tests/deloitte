using Deloitte.RestApi.Objects.Enums;

namespace Deloitte.RestApi.Resources.Get
{
    public class WeatherForecast
    {
        public string Description { get; set; }

        public decimal Temperature { get; set; }

        public decimal TemperatureMax { get; set; }

        public decimal TemperatureMin { get; set; }

        public TemperatureUnits TemperatureUnit { get; set; } = TemperatureUnits.Default;
    }
}