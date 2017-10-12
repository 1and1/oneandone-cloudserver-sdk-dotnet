using Newtonsoft.Json;

namespace OneAndOne.POCO.Response.Servers
{
    public class BaremetalModelHardware : HardwareBase
    {
        [JsonProperty(PropertyName = "core")]
        public int Core { get; set; }
    }
}
