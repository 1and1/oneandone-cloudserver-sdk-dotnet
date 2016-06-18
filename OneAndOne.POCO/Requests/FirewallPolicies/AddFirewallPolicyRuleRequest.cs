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
    public class AddFirewallPolicyRuleRequest
    {
        private List<RuleRequest> rules;
        [JsonProperty(PropertyName = "rules")]
        public List<RuleRequest> Rules
        {
            get { return rules; }
            set { rules = value; }
        }
    }

    public class RuleRequest
    {
        private string protocol;
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(PropertyName = "protocol")]
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
