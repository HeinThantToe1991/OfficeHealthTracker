using Newtonsoft.Json;
using System.Collections.Generic;

namespace OfficeHealthTracker.Interfaces.ViewModel
{
   
    public class AddressListViewModel
    {
        [JsonProperty("address")]
        public List<AddressViewModel> Address { get; set; }
    }
}
