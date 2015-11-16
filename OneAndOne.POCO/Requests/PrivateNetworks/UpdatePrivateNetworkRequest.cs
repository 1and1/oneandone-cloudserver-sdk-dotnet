using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Requests.PrivateNetworks
{
    public class UpdatePrivateNetworkRequest
    {
        private string name;
        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string description;
        [JsonProperty(PropertyName = "description")]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        private string network_address;
        [JsonProperty(PropertyName = "network_address")]
        public string NetworkAddress
        {
            get { return network_address; }
            set { network_address = value; }
        }
        private string subnet_mask;
        [JsonProperty(PropertyName = "subnet_mask")]
        public string SubnetMask
        {
            get { return subnet_mask; }
            set { subnet_mask = value; }
        }
    }
}
