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
    }
}
