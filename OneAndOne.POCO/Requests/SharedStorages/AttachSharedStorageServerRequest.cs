using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OneAndOne.POCO.Respones.SharedStorages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Requests.SharedStorages
{
    public class AttachSharedStorageServerRequest
    {
        private List<Server> servers;
        [JsonProperty(PropertyName = "servers")]
        public List<Server> Servers
        {
            get { return servers; }
            set { servers = value; }
        }
    }

    public class Server
    {
        private string id;
        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string rights;
        [JsonProperty(PropertyName = "rights")]
        [JsonConverter(typeof(StringEnumConverter))]
        public StorageServerRights Rights
        {
            get { return (StorageServerRights)Enum.Parse(typeof(StorageServerRights), rights); }
            set
            {
                rights = ((StorageServerRights)value).ToString();
            }
        }

    }
}
