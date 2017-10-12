using Newtonsoft.Json;

namespace OneAndOne.POCO.Response.Servers
{
    /// <summary>
    /// Server Main data 
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
