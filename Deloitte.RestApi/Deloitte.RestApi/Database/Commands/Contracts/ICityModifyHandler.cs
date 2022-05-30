using System.Threading.Tasks;
using Deloitte.RestApi.Database.Models;

namespace Deloitte.RestApi.Database.Commands.Contracts
{
    public interface ICityModifyHandler
    {
        Task<City> ExecuteAsync(City modifiedCity);
    }
}