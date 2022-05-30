using System.Threading.Tasks;
using Deloitte.RestApi.Services.Contracts;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dto = Deloitte.RestApi.Resources;

namespace Deloitte.RestApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;

        private readonly ILogger<CitiesController> _logger;

        public CitiesController(ICityService cityService, ILogger<CitiesController> logger)
        {
            _cityService = cityService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(Dto.Post.City city)
        {
            var newCity = await _cityService.CreateAsync(city);
            return Ok(newCity);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var removedCity = await _cityService.DeleteByCityIdAsync(id);
            if (removedCity == null) NotFound();

            return Ok(removedCity);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCityByIdAsync(int id)
        {
            var city = await _cityService.GetByCityIdAsync(id);
            if (city == null) NotFound();

            return Ok(city);
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<IActionResult> PatchAsync(int id, JsonPatchDocument<Dto.Patch.City> modifiedCity)
        {
            var updatedCity = await _cityService.ModifyByCityIdAsync(id, modifiedCity);
            return Ok(updatedCity);
        }

        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> SearchAsync(Dto.Post.CitySearchCriteria citySearchCriteria)
        {
            var cities = await _cityService.SearchAsync(citySearchCriteria.Name);
            return Ok(cities);
        }
    }
}