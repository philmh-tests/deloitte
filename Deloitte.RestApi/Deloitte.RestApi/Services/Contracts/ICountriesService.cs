using System.Collections.Generic;
using System.Threading.Tasks;
using Deloitte.RestApi.Services.Resources.Get;

namespace Deloitte.RestApi.Services.Contracts
{
    public interface ICountriesService
    {
        Task<IList<Country>> GetCountriesAsync(string nameTerm);
    }
}