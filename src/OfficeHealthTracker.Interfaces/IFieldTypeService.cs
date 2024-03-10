using System;
using OfficeHealthTracker.Interfaces.ViewModel;

namespace OfficeHealthTracker.Interfaces
{
    public interface IFieldTypeService
    {
        FieldTypeListViewModel GetAll();
        FieldTypeViewModel GetById(Guid id);
        FieldTypeViewModel GetByName(string name);
        FieldTypeViewModel Add(FieldTypeViewModel fieldType);
        FieldTypeViewModel Update(FieldTypeViewModel fieldType);
        void Delete(Guid id);
    }
}
