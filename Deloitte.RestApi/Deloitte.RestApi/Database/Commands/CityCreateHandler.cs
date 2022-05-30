using System.Threading.Tasks;
using Deloitte.RestApi.Database.Commands.Contracts;
using Deloitte.RestApi.Database.Models;

namespace Deloitte.RestApi.Database.Commands
{
    public class CityCreateHandler : ICityCreateHandler
    {
        private readonly DeloitteContext _dbContext;

        public CityCreateHandler(DeloitteContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<City> ExecuteAsync(City newCity)
        {
            var newCityEntryEntity = await _dbContext.Cities.AddAsync(newCity);
            await _dbContext.SaveChangesAsync();
            return newCityEntryEntity.Entity;
        }
    }
}