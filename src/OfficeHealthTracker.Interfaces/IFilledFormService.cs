using System;
using OfficeHealthTracker.Interfaces.ViewModel;

namespace OfficeHealthTracker.Interfaces
{
    public interface IFilledFormService
    {
        FilledFormListViewModel GetAll();
        FilledFormViewModel GetById(Guid id);
        FilledFormViewModel Add(FilledFormViewModel template);
        FilledFormViewModel Update(FilledFormViewModel template);
        void Delete(Guid id);
    }
}
