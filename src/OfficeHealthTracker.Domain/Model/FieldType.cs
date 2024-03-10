using System;
using System.ComponentModel.DataAnnotations;

namespace OfficeHealthTracker.Domain.Model
{
    public class FieldType

    {
        [Key]
        public Guid FieldTypeId { get; set; }

        public string TypeName { get; set; }
    }
}
