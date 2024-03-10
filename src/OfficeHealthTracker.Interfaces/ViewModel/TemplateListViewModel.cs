using Newtonsoft.Json;

using System.Collections.Generic;

namespace OfficeHealthTracker.Interfaces.ViewModel
{
    public class TemplateListViewModel
    {
        [JsonProperty("template")]
        public List<TemplateViewModel> Template { get; set; }
    }

    public class TemplateFieldListViewModel
    {
        [JsonProperty("templateField")]
        public List<TemplateFieldViewModel> TemplateField { get; set; }
    }
}
