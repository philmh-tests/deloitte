using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Deloitte.RestApi.Database.Commands.Contracts;
using Deloitte.RestApi.Database.Queries.Contracts;
using Deloitte.RestApi.Services;
using Deloitte.RestApi.Services.Contracts;
using Deloitte.RestApi.Tests.Helpers;
using Moq;
using Xunit;
using Entity = Deloitte.RestApi.Database.Models;
using ServiceDto = Deloitte.RestApi.Services.Resources;

namespace Deloitte.RestApi.Tests.Services
{
    public class CityServiceTests
    {
        private readonly Mock<ICityCreateHandler> _mockCityCreateHandler;
        private readonly Mock<ICityDeleteHandler> _mockCityDeleteHandler;
        private readonly Mock<ICityModifyHandler> _mockCityModifyHandler;
        private readonly Mock<ICityQueries> _mockCityQueries;
        private readonly Mock<ICountriesService> _mockCountriesService;
        private readonly Mock<IGeocodingService> _mockGeocodingService;
        private readonly Mock<IWeatherMapService> _mockWeatherMapService;

        protected CityServiceTests()
        {
            _mockCityCreateHandler = new Mock<ICityCreateHandler>();
            _mockCityDeleteHandler = new Mock<ICityDeleteHandler>();
            _mockCityModifyHandler = new Mock<ICityModifyHandler>();
            _mockCityQueries = new Mock<ICityQueries>();
            _mockCountriesService = new Mock<ICountriesService>();
            _mockGeocodingService = new Mock<IGeocodingService>();
            _mockWeatherMapService = new Mock<IWeatherMapService>();
        }

        private CityService CreateClassUnderTest()
        {
            return new CityService(_mockCityCreateHandler.Object, _mockCityDeleteHandler.Object,
                _mockCityModifyHandler.Object, _mockCityQueries.Object, _mockCountriesService.Object,
                _mockGeocodingService.Object, _mockWeatherMapService.Object);
        }

        public class SearchAsyncTests : CityServiceTests
        {
            [Theory]
            [InlineData(false)]
            [InlineData(true)]
            public async Task it_should_return_null_when_there_are_no_cities_matching_the_name_term(bool isEmptyList)
            {
                // Arrange
                const string noSuchCity = "NoSuchCity";

                var matchingCityEntities = isEmptyList ? new List<Entity.City>() : null;
                _mockCityQueries.Setup(x => x.SearchAsync(noSuchCity)).ReturnsAsync(matchingCityEntities);

                // Act
                var cityService = CreateClassUnderTest();
                var result = await cityService.SearchAsync(noSuchCity);

                // Assert
                Assert.Null(result);
            }

            [Fact]
            public async Task it_should_expand_the_country_codes_when_at_least_one_city_matches_the_name_term()
            {
                // Arrange
                const string validCity = DbCityEntityHelper.CityNameBath;
                const string validCountry = DbCityEntityHelper.CityCountryNameBath;

                var matchingCityEntities = new List<Entity.City>();
                var bathCityEntity = DbCityEntityHelper.CreateBathCityEntity();
                matchingCityEntities.Add(bathCityEntity);

                _mockCityQueries.Setup(x => x.SearchAsync(validCity)).ReturnsAsync(matchingCityEntities);

                var matchingCountryDtos = new List<ServiceDto.Get.Country>();
                var ukCountryDto = ServicesCountryDtoHelper.CreateUkCountryDto();
                matchingCountryDtos.Add(ukCountryDto);

                _mockCountriesService.Setup(x => x.GetCountriesAsync(validCountry)).ReturnsAsync(matchingCountryDtos);

                // Act
                var cityService = CreateClassUnderTest();
                var result = await cityService.SearchAsync(validCity);

                // Assert
                Assert.Single(result.Cities);
                var matchingCityDto = result.Cities.First();
                Assert.Equal(ServicesCountryDtoHelper.CountryCodeTwoCharUk, matchingCityDto.CountryCode.Cca2);
                Assert.Equal(ServicesCountryDtoHelper.CountryCodeThreeCharUk, matchingCityDto.CountryCode.Cca3);
                Assert.Equal(ServicesCountryDtoHelper.CountryCodeThreeNumUk, matchingCityDto.CountryCode.Ccn3);
            }
        }
    }
}