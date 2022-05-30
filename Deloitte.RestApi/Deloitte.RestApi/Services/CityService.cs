using System.Linq;
using System.Threading.Tasks;
using Deloitte.RestApi.Database.Commands.Contracts;
using Deloitte.RestApi.Database.Queries.Contracts;
using Deloitte.RestApi.Objects.Enums;
using Deloitte.RestApi.Services.Contracts;
using Microsoft.AspNetCore.JsonPatch;
using Dto = Deloitte.RestApi.Resources;
using Entity = Deloitte.RestApi.Database.Models;

namespace Deloitte.RestApi.Services
{
    public class CityService : ICityService
    {
        private readonly ICityCreateHandler _cityCreateHandler;
        private readonly ICityDeleteHandler _cityDeleteHandler;
        private readonly ICityModifyHandler _cityModifyHandler;
        private readonly ICityQueries _cityQueries;
        private readonly ICountriesService _countriesService;
        private readonly IGeocodingService _geocodingService;
        private readonly IWeatherMapService _weatherMapService;

        public CityService(ICityCreateHandler cityCreateHandler, ICityDeleteHandler cityDeleteHandler,
            ICityModifyHandler cityModifyHandler, ICityQueries cityQueries, ICountriesService countriesService,
            IGeocodingService geocodingService, IWeatherMapService weatherMapService)
        {
            _cityCreateHandler = cityCreateHandler;
            _cityDeleteHandler = cityDeleteHandler;
            _cityModifyHandler = cityModifyHandler;
            _cityQueries = cityQueries;
            _countriesService = countriesService;
            _geocodingService = geocodingService;
            _weatherMapService = weatherMapService;
        }

        public async Task<Dto.Get.CityExtended> GetByCityIdAsync(int cityId)
        {
            var matchingCityEntity = await _cityQueries.GetByCityIdAsync(cityId);
            if (matchingCityEntity == null) return null;

            // TODO: Introduce a "mapping" library (e.g. AutoMapper) to provide seamless mapping from one class to another.
            var matchingCityDto = new Dto.Get.CityExtended
            {
                Id = matchingCityEntity.Id,
                Country = matchingCityEntity.Country,
                DateEstablishedOn = matchingCityEntity.DateEstablishedOn,
                EstimatedPopulation = matchingCityEntity.EstimatedPopulation,
                Name = matchingCityEntity.Name,
                State = matchingCityEntity.State,
                TouristRating = matchingCityEntity.TouristRating
            };

            //await ExpandCountryCodeAsync(matchingCityDto);
            //await ExpandWeatherForecastAsync(matchingCityDto);

            return matchingCityDto;
        }

        public async Task<Dto.Get.CityExtendedCollection> SearchAsync(string nameTerm)
        {
            var matchingCityEntities = await _cityQueries.SearchAsync(nameTerm);
            if (matchingCityEntities == null || matchingCityEntities.Count == 0) return null;

            // TODO: Introduce a "mapping" library (e.g. AutoMapper) to provide seamless mapping from one class to another.
            var matchingCityDtos = matchingCityEntities.Select(x => new Dto.Get.CityExtended
            {
                Id = x.Id,
                Country = x.Country,
                DateEstablishedOn = x.DateEstablishedOn,
                EstimatedPopulation = x.EstimatedPopulation,
                Name = x.Name,
                State = x.State,
                TouristRating = x.TouristRating
            }).ToList();

            // Expand on the DTO response using external APIs.
            foreach (var matchingCityDto in matchingCityDtos)
            {
                await ExpandCountryCodeAsync(matchingCityDto);
                await ExpandWeatherForecastAsync(matchingCityDto);
            }

            var cityCollectionDto = new Dto.Get.CityExtendedCollection { Cities = matchingCityDtos };

            return cityCollectionDto;
        }

        public async Task<Dto.Get.CitySummary> CreateAsync(Dto.Post.City newCity)
        {
            // TODO: Validate the `newCity` contents and respond accordingly. Is the Name & Country valid? Can we determine a Latitude & Longitude for the City?

            // TODO: Introduce a "mapping" library (e.g. AutoMapper) to provide seamless mapping from one class to another.
            var newCityEntity = new Entity.City
            {
                Country = newCity.Country,
                DateEstablishedOn = newCity.DateEstablishedOn,
                EstimatedPopulation = newCity.EstimatedPopulation,
                Name = newCity.Name,
                State = newCity.State,
                TouristRating = newCity.TouristRating
            };


            var createdCityEntity = await _cityCreateHandler.ExecuteAsync(newCityEntity);

            // TODO: Introduce a "mapping" library (e.g. AutoMapper) to provide seamless mapping from one class to another.
            var createdCityDto = new Dto.Get.CitySummary
            {
                Id = createdCityEntity.Id,
                Country = createdCityEntity.Country,
                DateEstablishedOn = createdCityEntity.DateEstablishedOn,
                EstimatedPopulation = createdCityEntity.EstimatedPopulation,
                Name = createdCityEntity.Name,
                State = createdCityEntity.State,
                TouristRating = createdCityEntity.TouristRating
            };

            //await ExpandCountryCodeAsync(createdCityDto);
            //await ExpandWeatherForecastAsync(createdCityDto);

            return createdCityDto;
        }

        public async Task<Dto.Get.CitySummary> DeleteByCityIdAsync(int cityId)
        {
            var matchingCityEntity = await _cityDeleteHandler.ExecuteAsync(cityId);
            if (matchingCityEntity == null) return null;

            // TODO: Introduce a "mapping" library (e.g. AutoMapper) to provide seamless mapping from one class to another.
            var removedCityEntity = new Dto.Get.CitySummary
            {
                Id = matchingCityEntity.Id,
                Country = matchingCityEntity.Country,
                DateEstablishedOn = matchingCityEntity.DateEstablishedOn,
                EstimatedPopulation = matchingCityEntity.EstimatedPopulation,
                Name = matchingCityEntity.Name,
                State = matchingCityEntity.State,
                TouristRating = matchingCityEntity.TouristRating
            };

            //await ExpandCountryCodeAsync(removedCityEntity);
            //await ExpandWeatherForecastAsync(removedCityEntity);

            return removedCityEntity;
        }

        public async Task<Dto.Get.CitySummary> ModifyByCityIdAsync(int cityId,
            JsonPatchDocument<Dto.Patch.City> modifiedCity)
        {
            // TODO: Validate the `modifiedCity` contents and respond accordingly. Is the Name & Country valid? Can we determine a Latitude & Longitude for the City?

            var matchingCityDto = await GetByCityIdAsync(cityId);
            modifiedCity.ApplyTo(matchingCityDto);

            // TODO: Introduce a "mapping" library (e.g. AutoMapper) to provide seamless mapping from one class to another.
            var updatedCityEntity = new Entity.City
            {
                Id = matchingCityDto.Id,
                Country = matchingCityDto.Country,
                DateEstablishedOn = matchingCityDto.DateEstablishedOn,
                EstimatedPopulation = matchingCityDto.EstimatedPopulation,
                Name = matchingCityDto.Name,
                State = matchingCityDto.State,
                TouristRating = matchingCityDto.TouristRating
            };

            var modifiedCityEntity = await _cityModifyHandler.ExecuteAsync(updatedCityEntity);

            // TODO: Introduce a "mapping" library (e.g. AutoMapper) to provide seamless mapping from one class to another.
            var modifiedCityDto = new Dto.Get.CitySummary
            {
                Id = modifiedCityEntity.Id,
                Country = modifiedCityEntity.Country,
                DateEstablishedOn = modifiedCityEntity.DateEstablishedOn,
                EstimatedPopulation = modifiedCityEntity.EstimatedPopulation,
                Name = modifiedCityEntity.Name,
                State = modifiedCityEntity.State,
                TouristRating = modifiedCityEntity.TouristRating
            };

            //await ExpandCountryCodeAsync(modifiedCityDto);
            //await ExpandWeatherForecastAsync(modifiedCityDto);

            return modifiedCityDto;
        }

        private async Task ExpandCountryCodeAsync(Dto.Get.CityExtended matchingCityDto)
        {
            var matchingCountryDtos = await _countriesService.GetCountriesAsync(matchingCityDto.Country);
            if (matchingCountryDtos != null && matchingCountryDtos.Count > 0)
            {
                var matchingCountryDto = matchingCountryDtos.First();
                matchingCityDto.CountryCode.Cca2 = matchingCountryDto.Cca2;
                matchingCityDto.CountryCode.Cca3 = matchingCountryDto.Cca3;
                matchingCityDto.CountryCode.Ccn3 = matchingCountryDto.Ccn3;
                // Retrieving only the first Currency code for now; this can be CSV or a List at a later date.
                matchingCityDto.CurrencyCode = matchingCountryDto.Currencies.FirstOrDefault().Key;
            }
        }

        private async Task ExpandWeatherForecastAsync(Dto.Get.CityExtended matchingCityDto)
        {
            var matchingGeocodingDtos = await _geocodingService.GetGeocodingAsync(matchingCityDto.Name,
                matchingCityDto.State, matchingCityDto.CountryCode.Cca2);
            if (matchingGeocodingDtos != null && matchingGeocodingDtos.Count > 0)
            {
                var matchingGeocodingDto = matchingGeocodingDtos.First();
                var currentWeatherForecastDto = await _weatherMapService.GetCurrentWeatherAsync(
                    matchingGeocodingDto.Latitude,
                    matchingGeocodingDto.Longitude);
                if (currentWeatherForecastDto == null) return;

                var weatherDescriptions = currentWeatherForecastDto.Weather.Select(x => x.Main);
                matchingCityDto.WeatherForecast.Description = string.Join(",", weatherDescriptions);
                matchingCityDto.WeatherForecast.Temperature = currentWeatherForecastDto.Main.Temperature;
                matchingCityDto.WeatherForecast.TemperatureMin = currentWeatherForecastDto.Main.TemperatureMin;
                matchingCityDto.WeatherForecast.TemperatureMax = currentWeatherForecastDto.Main.TemperatureMax;
                // TODO: Make the unit of temperature configurable.
                matchingCityDto.WeatherForecast.TemperatureUnit = TemperatureUnits.Celsius;
            }
        }
    }
}