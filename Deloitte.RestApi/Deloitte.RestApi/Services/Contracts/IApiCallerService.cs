using System.Threading.Tasks;

namespace Deloitte.RestApi.Services.Contracts
{
    public interface IApiCallerService
    {
        Task<string> GetAsync(string requestUri);
    }
}