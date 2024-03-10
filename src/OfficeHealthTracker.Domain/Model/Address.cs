using System;
using System.ComponentModel.DataAnnotations;

namespace OfficeHealthTracker.Domain.Model
{
    public class Address
    {
        [Key]
        public Guid AddressId { get; set; }

        public string Building { get; set; }

        public string Level { get; set; }

        public string Room { get; set; }

        public string CarPark { get; set; }

        public string Lobby { get; set; }

        public string Pantry { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }



        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

        public string PostalCode { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
