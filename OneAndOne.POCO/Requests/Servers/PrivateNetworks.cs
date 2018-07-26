using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Requests.Servers
{
    public class PrivateNetworks
    {
        private string id;
        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;
        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string serverIp;
        [JsonProperty(PropertyName = "server_ip")]
        public string ServerIp
        {
            get { return serverIp; }
            set { serverIp = value; }
        }

        private string public_name;
        [JsonProperty(PropertyName = "public_name")]
        public string PublicName
        {
            get { return public_name; }
            set { public_name = value; }
        }
    }
}
