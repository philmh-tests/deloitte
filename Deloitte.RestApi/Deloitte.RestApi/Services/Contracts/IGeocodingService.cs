using System.Collections.Generic;
using System.Threading.Tasks;
using Deloitte.RestApi.Services.Resources.Get;

namespace Deloitte.RestApi.Services.Contracts
{
    public interface IGeocodingService
    {
        Task<IList<Geocoding>> GetGeocodingAsync(string cityName, string state, string cca2);
    }
}