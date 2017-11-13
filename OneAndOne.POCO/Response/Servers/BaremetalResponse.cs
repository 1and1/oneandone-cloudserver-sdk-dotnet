using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OneAndOne.POCO.Response.Servers
{
    /// <summary>
    /// Baremetal server data 
    /// </summary>
    public class BaremetalResponse
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "hardware")]
        public BaremetalModelHardware Hardware { get; set; }
    }
}
