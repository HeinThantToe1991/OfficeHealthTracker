using System;
using Newtonsoft.Json;

namespace OfficeHealthTracker.Interfaces.ViewModel
{
    public class TemplateViewModel
    {
        public Guid TemplateId { get; set; }

        public string TemplateName { get; set; }

      
        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }

    public class TemplateRequest
    {
        [JsonProperty("template")]
        public TemplateViewModel Template { get; set; }
        public TemplateFieldListViewModel FieldList { get; set; }
    }

    public class TemplateResponse
    {
        [JsonProperty("template")]
        public TemplateViewModel Template { get; set; }

        [JsonProperty("templateField")]
        public TemplateFieldListViewModel FieldList { get; set; }
    }
}
