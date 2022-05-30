using System;

namespace Deloitte.RestApi.Resources.Patch
{
    public class City
    {
        public DateTime? DateEstablishedOn { get; set; }

        public long? EstimatedPopulation { get; set; }

        public byte? TouristRating { get; set; }
    }
}