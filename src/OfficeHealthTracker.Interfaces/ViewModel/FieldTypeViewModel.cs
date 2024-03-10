using System.ComponentModel.DataAnnotations;
using System;

namespace OfficeHealthTracker.Interfaces.ViewModel
{
    public class FieldTypeViewModel
    {
        [Key]
        public Guid FieldTypeId { get; set; }

        public string TypeName { get; set; }
    }
}
