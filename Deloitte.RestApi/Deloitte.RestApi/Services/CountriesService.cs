using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Deloitte.RestApi.Services.Contracts;
using Deloitte.RestApi.Services.Resources.Get;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Deloitte.RestApi.Services
{
    public class CountriesService : ICountriesService
    {
        private readonly IApiCallerService _apiCallerService;
        private readonly IMemoryCache _memoryCache;

        public CountriesService(IApiCallerService apiCallerService, IMemoryCache memoryCache)
        {
            _apiCallerService = apiCallerService;
            _memoryCache = memoryCache;
        }

        public async Task<IList<Country>> GetCountriesAsync(string nameTerm)
        {
            var cacheKey = $"{GetType().Name}.{nameof(GetCountriesAsync)}:{nameTerm}";

            if (_memoryCache.TryGetValue(cacheKey, out var countryDtos))
            {
                return (List<Country>)countryDtos;
            }

            var jsonResponse = await _apiCallerService.GetAsync($"https://restcountries.com/v3.1/name/{nameTerm}");
            countryDtos = JsonConvert.DeserializeObject<List<Country>>(jsonResponse);

            _memoryCache.Set(cacheKey, countryDtos, TimeSpan.FromMinutes(5));

            return (List<Country>)countryDtos;
        }
    }
}