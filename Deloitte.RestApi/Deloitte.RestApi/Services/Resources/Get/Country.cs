using System.Collections.Generic;

namespace Deloitte.RestApi.Services.Resources.Get
{
    public class Country
    {
        public string Cca2 { get; set; }

        public string Cca3 { get; set; }

        public int Ccn3 { get; set; }

        public Dictionary<string, CountryCurrency> Currencies { get; set; }
    }
}