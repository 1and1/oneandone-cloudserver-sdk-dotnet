using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Respones.PrivateNetworks
{
    public class PrivateNetworksResponse
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string network_address { get; set; }
        public string subnet_mask { get; set; }
        public string state { get; set; }
        public string creation_date { get; set; }
        public List<object> servers { get; set; }
        public string cloudpanel_id { get; set; }
    }
}
