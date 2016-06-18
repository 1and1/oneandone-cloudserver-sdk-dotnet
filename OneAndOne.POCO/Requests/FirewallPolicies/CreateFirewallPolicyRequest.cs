using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OneAndOne.POCO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Requests.FirewallPolicies
{
    public class CreateFirewallPolicyRequest
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

        private List<CreateFirewallPocliyRule> rules;
        [JsonProperty(PropertyName = "rules")]
        public List<CreateFirewallPocliyRule> Rules
        {
            get { return rules; }
            set { rules = value; }
        }
    }

    public class CreateFirewallPocliyRule
    {
        private string protocol;
        [JsonProperty(PropertyName = "protocol")]
        [JsonConverter(typeof(StringEnumConverter))]
        public RuleProtocol Protocol
        {
            get { return (RuleProtocol)Enum.Parse(typeof(RuleProtocol), protocol); }
            set
            {
                protocol = ((RuleProtocol)value).ToString();
            }
        }

        private int port_from;
        [JsonProperty(PropertyName = "port_from")]
        public int PortFrom
        {
            get { return port_from; }
            set { port_from = value; }
        }
        private int port_to;
        [JsonProperty(PropertyName = "port_to")]
        public int PortTo
        {
            get { return port_to; }
            set { port_to = value; }
        }
        private string source;
        [JsonProperty(PropertyName = "source")]
        public string Source
        {
            get { return source; }
            set { source = value; }
        }
    }
}
