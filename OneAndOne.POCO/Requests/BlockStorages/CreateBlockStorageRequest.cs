using Newtonsoft.Json;

namespace OneAndOne.POCO.Requests.BlockStorages
{
    public class CreateBlockStorageRequest
    {
        /// <summary>
        /// Name of the block storage
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Size of the block storage. minimum: 20, maximum: 500, multipleOf: 10
        /// </summary>
        [JsonProperty(PropertyName = "size")]
        public int Size { get; set; }

        /// <summary>
        /// Description of the block storage
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// ID of the datacenter where the block storage will be created
        /// </summary>
        [JsonProperty(PropertyName = "datacenter_id")]
        public string DatacenterId { get; set; }

        /// <summary>
        /// ID of the server that will be attached to the block storage
        /// </summary>
        [JsonProperty(PropertyName = "server_id")]
        public string ServerId { get; set; }
    }
}
