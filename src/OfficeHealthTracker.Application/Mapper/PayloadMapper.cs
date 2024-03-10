using System;
using System.Collections.Generic;
using OfficeHealthTracker.Domain.Model;
using OfficeHealthTracker.Interfaces.ViewModel;

namespace OfficeHealthTracker.Application.Mapper
{
    public static class PayloadMapper
    {
      
        public static PayloadData ToDbModel(PayloadViewModel viewModel)
        {
            var model = new PayloadData
            {
                PayLoadId = Guid.NewGuid(),
                DeviceId = viewModel.DeviceId,
                Humidity = viewModel.Data.Humidity,
                Occupancy = viewModel.Data.Occupancy,
                Temperature = viewModel.Data.Temperature,

            };
            return model;
        }
    }

}
