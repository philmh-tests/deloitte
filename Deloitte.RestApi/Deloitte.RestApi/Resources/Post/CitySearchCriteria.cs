using System.ComponentModel.DataAnnotations;

namespace Deloitte.RestApi.Resources.Post

{
    public class CitySearchCriteria
    {
        [Required] public string Name { get; set; }
    }
}