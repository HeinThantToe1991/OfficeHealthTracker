using System;
using System.ComponentModel.DataAnnotations;

namespace OfficeHealthTracker.Domain.Model
{
    public class PayloadData

    {
        [Key]
        public Guid PayLoadId { get; set; }
        public string DeviceId { get; set; }
        public int Temperature { get; set; }
        public int Humidity { get; set; }
        public bool Occupancy { get; set; }
    }
}
