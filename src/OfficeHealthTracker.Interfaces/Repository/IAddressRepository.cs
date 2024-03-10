using System;
using System.Collections.Generic;
using System.Text;
using OfficeHealthTracker.Domain.Model;

namespace OfficeHealthTracker.Interfaces.Repository
{
    public interface IAddressRepository
    {
        void Add(Address data);

        void Delete(Guid id);

        void Update(Address data);

        List<Address> GetAll();
        Address GetAddressById(Guid id);
    }
}
