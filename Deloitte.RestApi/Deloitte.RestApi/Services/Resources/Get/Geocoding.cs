using Newtonsoft.Json;

namespace Deloitte.RestApi.Services.Resources.Get
{
    public class Geocoding
    {
        [JsonProperty("country")] public string CountryCode { get; set; }

        [JsonProperty("lat")] public decimal Latitude { get; set; }

        [JsonProperty("lon")] public decimal Longitude { get; set; }

        public string Name { get; set; }

        public string State { get; set; }
    }
}