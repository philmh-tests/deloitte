using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Deloitte.RestApi.Database.Models
{
    [Table("City")]
    public class City
    {
        public string Country { get; set; }

        public DateTime? DateEstablishedOn { get; set; }

        public long? EstimatedPopulation { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string State { get; set; }

        public byte? TouristRating { get; set; }
    }
}