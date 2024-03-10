using System;
using System.Collections.Generic;
using System.Text;
using OfficeHealthTracker.Domain.Model;

namespace OfficeHealthTracker.Interfaces.Repository
{
    public interface IPayloadRepository
    {
        void Add(PayloadData data);
    }
}
