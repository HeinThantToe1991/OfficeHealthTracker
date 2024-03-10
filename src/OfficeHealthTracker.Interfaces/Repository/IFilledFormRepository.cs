using System;
using System.Collections.Generic;
using System.Text;
using OfficeHealthTracker.Domain.Model;

namespace OfficeHealthTracker.Interfaces.Repository
{
    public interface IFilledFormRepository
    {
        void Add(FilledForm data);
        void Delete(Guid id);
        void Update(FilledForm data);
        List<FilledForm> GetAll();
        FilledForm GetById(Guid id);
    }
}
