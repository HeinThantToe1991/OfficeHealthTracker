using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OfficeHealthTracker.Domain.Model
{
    public class Template

    {
        [Key]
        public Guid TemplateId { get; set; }

        public string TemplateName { get; set; }

        public ICollection<TemplateField> TemplateFields { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
