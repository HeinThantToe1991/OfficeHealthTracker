using System.ComponentModel.DataAnnotations;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace OfficeHealthTracker.Interfaces.ViewModel
{
    public class FieldTypeListViewModel
    {
        [JsonProperty("fieldType")]
        public List<FieldTypeViewModel> FieldType { get; set; }
    }
}
