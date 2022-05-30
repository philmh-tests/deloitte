using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Deloitte.RestApi.Database.Models;
using Deloitte.RestApi.Database.Queries.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Deloitte.RestApi.Database.Queries
{
    public class CityQueries : ICityQueries
    {
        private readonly DeloitteContext _dbContext;

        public CityQueries(DeloitteContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<City> GetByCityIdAsync(int id)
        {
            var cityEntity = await _dbContext.Cities.SingleOrDefaultAsync(x => x.Id == id);
            return cityEntity;
        }

        public async Task<IList<City>> SearchAsync(string nameTerm)
        {
            var cities = await _dbContext.Cities.Where(x => x.Name.Contains(nameTerm)).ToListAsync();
            return cities;
        }
    }
}