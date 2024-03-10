using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OfficeHealthTracker.Interfaces.ViewModel
{
   
    public class FilledFormListViewModel
    {
        [JsonProperty("filledForm")]
        public List<FilledFormViewModel> FilledForm;
    }
}
