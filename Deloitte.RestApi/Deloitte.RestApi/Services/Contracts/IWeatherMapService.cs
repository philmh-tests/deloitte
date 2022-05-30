using System.Threading.Tasks;
using Deloitte.RestApi.Services.Resources.Get;

namespace Deloitte.RestApi.Services.Contracts
{
    public interface IWeatherMapService
    {
        Task<WeatherForecast> GetCurrentWeatherAsync(decimal latitude, decimal longitude);
    }
}