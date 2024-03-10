using System;
using OfficeHealthTracker.Interfaces.ViewModel;

namespace OfficeHealthTracker.Interfaces
{
    public interface ITemplateService
    {
        TemplateListViewModel GetAll();
        TemplateResponse GetById(Guid id);
        TemplateViewModel GetByName(string name);
        TemplateViewModel Add(TemplateViewModel template,TemplateFieldListViewModel templateField);
        TemplateViewModel Update(TemplateViewModel template, TemplateFieldListViewModel templateField);
        void Delete(Guid id);
    }
}
