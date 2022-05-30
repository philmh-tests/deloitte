using System.Collections.Generic;

namespace Deloitte.RestApi.Resources.Get
{
    public class CityExtendedCollection
    {
        public IList<CityExtended> Cities { get; set; } = new List<CityExtended>();
    }
}