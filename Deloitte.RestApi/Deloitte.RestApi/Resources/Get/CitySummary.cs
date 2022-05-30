namespace Deloitte.RestApi.Resources.Get
{
    public class CitySummary : Patch.City
    {
        public string Country { get; set; }

        public string CurrencyCode { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string State { get; set; }
    }
}