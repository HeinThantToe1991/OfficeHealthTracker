using System;
using OfficeHealthTracker.Interfaces.ViewModel;

namespace OfficeHealthTracker.Interfaces
{
    public interface IPayloadService
    {
 
        PayloadViewModel Add(PayloadViewModel fieldType);
    }
}
