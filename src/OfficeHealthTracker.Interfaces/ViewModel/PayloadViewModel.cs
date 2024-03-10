using OfficeHealthTracker.Domain.Model;
using System;

namespace OfficeHealthTracker.Interfaces.ViewModel
{
    public class PayloadViewModel
    {
        public string DeviceId { get; set; }
        public string DeviceType { get; set; }
        public string DeviceName { get; set; }
        public string GroupId { get; set; }
        public string DataType { get; set; }
        public PayloadDataViewModel Data { get; set; }
        public long Timestamp { get; set; }
    }
}
