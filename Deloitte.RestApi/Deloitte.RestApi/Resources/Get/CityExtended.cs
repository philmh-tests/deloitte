namespace Deloitte.RestApi.Resources.Get
{
    public class CityExtended : CitySummary
    {
        public CountryCode CountryCode { get; set; } = new CountryCode();

        public WeatherForecast WeatherForecast { get; set; } = new WeatherForecast();
    }
}