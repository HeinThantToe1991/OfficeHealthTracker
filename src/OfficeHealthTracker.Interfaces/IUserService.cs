using OfficeHealthTracker.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OfficeHealthTracker.Interfaces
{
    public interface IUserService
    {
        void RegisterUser(User user);
    }
}
