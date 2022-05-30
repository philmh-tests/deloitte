using System;
using System.ComponentModel.DataAnnotations;

namespace Deloitte.RestApi.Resources.Post

{
    public class City
    {
        [Required] public string Country { get; set; }

        public DateTime? DateEstablishedOn { get; set; }

        public long? EstimatedPopulation { get; set; }

        [Required] public string Name { get; set; }

        [Required] public string State { get; set; }

        public byte? TouristRating { get; set; }
    }
}