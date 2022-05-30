using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Deloitte.RestApi.Services.Contracts;
using Deloitte.RestApi.Services.Resources.Get;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Deloitte.RestApi.Services
{
    public class GeocodingService : IGeocodingService
    {
        private readonly IApiCallerService _apiCallerService;
        private readonly IMemoryCache _memoryCache;

        public GeocodingService(IApiCallerService apiCallerService, IMemoryCache memoryCache)
        {
            _apiCallerService = apiCallerService;
            _memoryCache = memoryCache;
        }

        public async Task<IList<Geocoding>> GetGeocodingAsync(string cityName, string state, string cca2)
        {
            if (string.IsNullOrWhiteSpace(cityName))
            {
                throw new ArgumentNullException(nameof(cityName));
            }

            var cacheKey = $"{GetType().Name}.{nameof(GetGeocodingAsync)}:{cityName};{state};{cca2}";

            if (_memoryCache.TryGetValue(cacheKey, out var geocodingDtos))
            {
                return (List<Geocoding>)geocodingDtos;
            }

            // Build the search term which is of the format: 
            var searchTerm = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(cityName))
            {
                if (searchTerm.Length > 0) searchTerm.Append(",");
                searchTerm.Append(cityName);
            }

            // The state term is only valid for the United States of America.
            if (cca2 == "US" && !string.IsNullOrWhiteSpace(state))
            {
                if (searchTerm.Length > 0) searchTerm.Append(",");
                searchTerm.Append(state);
            }

            if (!string.IsNullOrWhiteSpace(cca2))
            {
                if (searchTerm.Length > 0) searchTerm.Append(",");
                searchTerm.Append(cca2);
            }

            // TODO: Retrieve the API key from the appsettings.json rather than via a hard-coded literal.
            var jsonResponse = await _apiCallerService.GetAsync(
                $"https://api.openweathermap.org/geo/1.0/direct?appid={Startup.OpenWeatherMapApiKey}&q={searchTerm}");
            geocodingDtos = JsonConvert.DeserializeObject<List<Geocoding>>(jsonResponse);

            _memoryCache.Set(cacheKey, geocodingDtos, TimeSpan.FromMinutes(5));

            return (List<Geocoding>)geocodingDtos;
        }
    }
}