using Entity = Deloitte.RestApi.Database.Models;

namespace Deloitte.RestApi.Tests.Helpers
{
    internal static class DbCityEntityHelper
    {
        public const string CityNameBath = "Bath";
        public const string CityStateBath = "Somerset";
        public const string CityCountryNameBath = "United Kingdom";
        public const long EstimatedPopulationBath = 104106;
        public const byte TouristRatingBath = 5;

        public static Entity.City CreateBathCityEntity()
        {
            return new Entity.City
            {
                Id = 1, Name = CityNameBath, State = CityStateBath, Country = CityCountryNameBath,
                EstimatedPopulation = EstimatedPopulationBath,
                TouristRating = TouristRatingBath
            };
        }
    }
}