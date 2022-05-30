using System.Collections.Generic;

namespace Deloitte.RestApi.Services.Resources.Get
{
    public class WeatherForecast
    {
        public WeatherForecastMain Main { get; set; }
        public ICollection<WeatherForecastWeather> Weather { get; set; }
    }
}