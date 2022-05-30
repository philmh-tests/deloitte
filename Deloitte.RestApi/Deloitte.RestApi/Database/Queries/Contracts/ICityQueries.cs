using System.Collections.Generic;
using System.Threading.Tasks;
using Deloitte.RestApi.Database.Models;

namespace Deloitte.RestApi.Database.Queries.Contracts
{
    public interface ICityQueries
    {
        Task<City> GetByCityIdAsync(int id);

        Task<IList<City>> SearchAsync(string nameTerm);
    }
}