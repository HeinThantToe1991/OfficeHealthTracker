using OfficeHealthTracker.Domain.Model;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace OfficeHealthTracker.Interfaces.ViewModel
{
    public class TemplateFieldViewModel
    {
        public Guid TemplateFieldId { get; set; }
        public Guid TemplateId { get; set; }
        public Guid FieldTypeId { get; set; }
        public string FieldOptions { get; set; }
        public int Sorting { get; set; }
        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
