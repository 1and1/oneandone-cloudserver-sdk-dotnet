using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Requests.PrivateNetworks
{
    public class AttachPrivateNetworkServersRequest
    {
        private List<string> servers;
        [JsonProperty(PropertyName = "servers")]
        public List<string> Servers
        {
            get { return servers; }
            set { servers = value; }
        }
    }
}
