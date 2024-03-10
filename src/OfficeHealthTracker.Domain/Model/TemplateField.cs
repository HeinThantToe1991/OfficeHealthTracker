using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OfficeHealthTracker.Domain.Model
{
    public class TemplateField

    {
        [Key]
        public Guid TemplateFieldId { get; set; }
        public Guid TemplateId { get; set; }

        public Template Template { get; set; }
        public Guid FieldTypeId { get; set; }

        public FieldType FieldType { get; set; }

        // FieldOptions could be stored as JSON string or in a related table, depending on the complexity
        public string FieldOptions { get; set; }

        public int  Sorting{ get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
