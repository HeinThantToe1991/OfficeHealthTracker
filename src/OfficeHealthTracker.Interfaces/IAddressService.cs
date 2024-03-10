using System;
using OfficeHealthTracker.Interfaces.ViewModel;

namespace OfficeHealthTracker.Interfaces
{
    public interface IAddressService
    {
        AddressListViewModel GetAll();
        AddressViewModel GetById(Guid id);
        AddressViewModel Add(AddressViewModel address);
        AddressViewModel Update(AddressViewModel address);
        void Delete(Guid id);
    }
}
