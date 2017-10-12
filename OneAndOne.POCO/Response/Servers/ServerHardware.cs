using Newtonsoft.Json;

namespace OneAndOne.POCO.Response.Servers
{
    public class ServerHardware : HardwareBase
    {
        [JsonProperty(PropertyName = "fixed_instance_size_id")]
        public string FixedInstanceSizeId { get; set; }

        [JsonProperty(PropertyName = "baremetal_model_id")]
        public string BaremetalModelId { get; set; }

        [JsonProperty(PropertyName = "vcore")]
        public int Vcore { get; set; }
    }
}
