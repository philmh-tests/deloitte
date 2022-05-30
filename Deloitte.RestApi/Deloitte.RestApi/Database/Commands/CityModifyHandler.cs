using System.Threading.Tasks;
using Deloitte.RestApi.Database.Commands.Contracts;
using Deloitte.RestApi.Database.Models;

namespace Deloitte.RestApi.Database.Commands
{
    public class CityModifyHandler : ICityModifyHandler
    {
        private readonly DeloitteContext _dbContext;

        public CityModifyHandler(DeloitteContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<City> ExecuteAsync(City modifiedCity)
        {
            var modifiedCityEntryEntity = _dbContext.Cities.Update(modifiedCity);
            await _dbContext.SaveChangesAsync();
            return modifiedCityEntryEntity.Entity;
        }
    }
}