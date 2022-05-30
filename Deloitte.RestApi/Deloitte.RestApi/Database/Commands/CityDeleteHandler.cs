using System.Threading.Tasks;
using Deloitte.RestApi.Database.Commands.Contracts;
using Deloitte.RestApi.Database.Models;

namespace Deloitte.RestApi.Database.Commands
{
    public class CityDeleteHandler : ICityDeleteHandler
    {
        private readonly DeloitteContext _dbContext;

        public CityDeleteHandler(DeloitteContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<City> ExecuteAsync(int cityId)
        {
            var cityEntityToDelete = await _dbContext.Cities.FindAsync(cityId);
            if (cityEntityToDelete == null) return null;

            var removedCityEntryEntity = _dbContext.Cities.Remove(cityEntityToDelete);
            await _dbContext.SaveChangesAsync();
            return removedCityEntryEntity.Entity;
        }
    }
}