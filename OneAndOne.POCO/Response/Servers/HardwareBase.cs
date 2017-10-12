using Newtonsoft.Json;
using System.Collections.Generic;

namespace OneAndOne.POCO.Response.Servers
{
    public class HardwareBase
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "cores_per_processor")]
        public int CoresPerProcessor { get; set; }

        [JsonProperty(PropertyName = "ram")]
        public float Ram { get; set; }

        [JsonProperty(PropertyName = "hdds")]
        public List<Hdd> Hdds { get; set; }
    }
}
