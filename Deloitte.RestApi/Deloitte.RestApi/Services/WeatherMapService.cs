using System;
using System.Threading.Tasks;
using Deloitte.RestApi.Services.Contracts;
using Deloitte.RestApi.Services.Resources.Get;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Deloitte.RestApi.Services
{
    public class WeatherMapService : IWeatherMapService
    {
        private readonly IApiCallerService _apiCallerService;
        private readonly IMemoryCache _memoryCache;

        public WeatherMapService(IApiCallerService apiCallerService, IMemoryCache memoryCache)
        {
            _apiCallerService = apiCallerService;
            _memoryCache = memoryCache;
        }

        public async Task<WeatherForecast> GetCurrentWeatherAsync(decimal latitude, decimal longitude)
        {
            var cacheKey = $"{GetType().Name}.{nameof(GetCurrentWeatherAsync)}:{latitude};{longitude}";

            if (_memoryCache.TryGetValue(cacheKey, out var weatherForecastDtos))
            {
                return (WeatherForecast)weatherForecastDtos;
            }

            // TODO: Retrieve the API key from the appsettings.json rather than via a hard-coded literal.
            var jsonResponse = await _apiCallerService.GetAsync(
                $"https://api.openweathermap.org/data/2.5/weather?appid={Startup.OpenWeatherMapApiKey}&lat={latitude}&lon={longitude}&units=metric");
            weatherForecastDtos = JsonConvert.DeserializeObject<WeatherForecast>(jsonResponse);

            _memoryCache.Set(cacheKey, weatherForecastDtos, TimeSpan.FromMinutes(5));

            return (WeatherForecast)weatherForecastDtos;
        }
    }
}