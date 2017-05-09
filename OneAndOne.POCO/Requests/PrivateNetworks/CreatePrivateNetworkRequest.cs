using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Requests.PrivateNetworks
{
    public class CreatePrivateNetworkRequest
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
        /// <summary>
        /// Required: Private network name "maxLength": 128,
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "network_address")]
        /// <summary>
        ///  NOT Required: Private network address (valid IP)
        /// </summary>
        /// 
        public string NetworkAddress
        {
            get { return network_address; }
            set { network_address = value; }
        }
        private string subnet_mask;
        [JsonProperty(PropertyName = "subnet_mask")]
        /// <summary>
        ///  NOT Required: Subnet mask (valid subnet for the given IP)
        /// </summary>
        /// 
        public string SubnetMask
        {
            get { return subnet_mask; }
            set { subnet_mask = value; }
        }

        ///<summary>
        //datacenter_id 
        //</summary>
        private string datacenter_id;
        [JsonProperty(PropertyName = "datacenter_id")]

        public string DatacenterId
        {
            get
            { return datacenter_id; }

            set
            { datacenter_id = value; }
        }
    }
}
