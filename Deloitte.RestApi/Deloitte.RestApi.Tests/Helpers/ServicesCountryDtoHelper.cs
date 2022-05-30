using System.Collections.Generic;
using ServiceDto = Deloitte.RestApi.Services.Resources;

namespace Deloitte.RestApi.Tests.Helpers
{
    internal static class ServicesCountryDtoHelper
    {
        public const string CurrencyCodeUk = "GBP";
        public const string CountryCodeTwoCharUk = "GB";
        public const string CountryCodeThreeCharUk = "GBR";
        public const int CountryCodeThreeNumUk = 826;

        public static ServiceDto.Get.Country CreateUkCountryDto()
        {
            var ukCurrencyDtos = new Dictionary<string, ServiceDto.Get.CountryCurrency>();
            var ukCurrencyDto = new ServiceDto.Get.CountryCurrency
            {
                Name = "British pound",
                Symbol = "£"
            };
            ukCurrencyDtos.Add(CurrencyCodeUk, ukCurrencyDto);

            return new ServiceDto.Get.Country
            {
                Cca2 = CountryCodeTwoCharUk,
                Cca3 = CountryCodeThreeCharUk,
                Ccn3 = CountryCodeThreeNumUk,
                Currencies = ukCurrencyDtos
            };
        }
    }
}