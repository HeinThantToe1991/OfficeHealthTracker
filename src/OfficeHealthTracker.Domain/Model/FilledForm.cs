using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OfficeHealthTracker.Domain.Model
{
    public class FilledForm

    {
        [Key]
        public Guid FilledFormId { get; set; }

        [ForeignKey("Template")]
        public Guid TemplateId { get; set; }

        // FieldValues could be stored as JSON string or in a related table, depending on the complexity
        public string FieldValues { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
