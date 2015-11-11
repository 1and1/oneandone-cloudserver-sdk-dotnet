using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Requests.Servers
{
    public class UpdateServerImageRequest
    {
        private string id;
        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string password;
        [JsonProperty(PropertyName = "password")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        private UpdateFirewallPolicy firewall_policy;
        [JsonProperty(PropertyName = "firewall_policy")]
        public UpdateFirewallPolicy Firewall_policy
        {
            get { return firewall_policy; }
            set { firewall_policy = value; }
        }
    }

    public class UpdateFirewallPolicy
    {
        private string id;
        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

    }
}
