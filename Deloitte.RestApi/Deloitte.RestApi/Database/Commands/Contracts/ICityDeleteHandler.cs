using System.Threading.Tasks;
using Deloitte.RestApi.Database.Models;

namespace Deloitte.RestApi.Database.Commands.Contracts
{
    public interface ICityDeleteHandler
    {
        Task<City> ExecuteAsync(int cityId);
    }
}