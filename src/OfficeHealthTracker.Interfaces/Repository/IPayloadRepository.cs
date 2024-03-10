using System;
using System.Collections.Generic;
using System.Text;
using OfficeHealthTracker.Domain.Model;

namespace OfficeHealthTracker.Interfaces.Repository
{
    public interface IFieldTypeRepository
    {
        void Add(FieldType data);
        void Delete(Guid id);
        void Update(FieldType data);
        List<FieldType> GetAll();
        FieldType GetById(Guid id);
        FieldType GetByName(string name);
    }
}
