using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OfficeHealthTracker.Interfaces.ViewModel
{
   
    public class FilledFormViewModel
    {
        public Guid FilledFormId { get; set; }

        public Guid TemplateId { get; set; }

        public string FieldValues { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
