using Newtonsoft.Json;

namespace OneAndOne.POCO.Requests.BlockStorages
{
    public class UpdateBlockStorageRequest
    {
        /// <summary>
        /// Name of the block storage
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Description of the block storage
        /// </summary>
        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Size of the block storage. minimum: 20, maximum: 500, multipleOf: 10
        /// </summary>
        [JsonProperty(PropertyName = "size")]
        public int Size { get; set; }
    }
}
