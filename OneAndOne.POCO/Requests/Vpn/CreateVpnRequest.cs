using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Requests.Vpn
{
    public class CreateVpnRequest
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

        private string datacenter_id;
        [JsonProperty(PropertyName = "datacenter_id")]
        public string Datacenterid
        {
            get { return datacenter_id; }

            set { datacenter_id = value; }
        }
    }
}
