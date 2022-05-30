using System.Threading.Tasks;
using Microsoft.AspNetCore.JsonPatch;
using Dto = Deloitte.RestApi.Resources;

namespace Deloitte.RestApi.Services.Contracts
{
    public interface ICityService
    {
        Task<Dto.Get.CitySummary> CreateAsync(Dto.Post.City newCity);

        Task<Dto.Get.CitySummary> DeleteByCityIdAsync(int cityId);

        Task<Dto.Get.CityExtended> GetByCityIdAsync(int cityId);

        Task<Dto.Get.CitySummary> ModifyByCityIdAsync(int cityId, JsonPatchDocument<Dto.Patch.City> modifiedCity);

        Task<Dto.Get.CityExtendedCollection> SearchAsync(string nameTerm);
    }
}