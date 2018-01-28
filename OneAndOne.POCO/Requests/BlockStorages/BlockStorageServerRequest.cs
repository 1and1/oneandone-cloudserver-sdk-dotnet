using Newtonsoft.Json;

namespace OneAndOne.POCO.Requests.BlockStorages
{
    public class BlockStorageServerRequest
    {
        /// <summary>
        /// Id of the server that will be attached to the block storage
        /// </summary>
        [JsonProperty(PropertyName = "server")]
        public string ServerId { get; set; }
    }
}
