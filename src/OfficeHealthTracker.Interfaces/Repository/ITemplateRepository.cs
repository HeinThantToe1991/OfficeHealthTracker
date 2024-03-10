using OfficeHealthTracker.Domain.Model;
using System;
using System.Collections.Generic;

namespace OfficeHealthTracker.Interfaces.Repository
{
    public interface ITemplateRepository
    {
        void Add(Template data, List<TemplateField> templateField);
        void Delete(Guid id);
        void Update(Template data, List<TemplateField> templateField);
        List<Template> GetAll();
        Template GetById(Guid id);

        Template GetByName(string name);

        void DeleteTemplateFieldByTemplateId(Guid id);
    }
}
