using System;

namespace OfficeHealthTracker.Interfaces.ViewModel
{
    public class PayloadDataViewModel
    {
        public int Temperature { get; set; }
        public int Humidity { get; set; }
        public bool Occupancy { get; set; }
    }
}
