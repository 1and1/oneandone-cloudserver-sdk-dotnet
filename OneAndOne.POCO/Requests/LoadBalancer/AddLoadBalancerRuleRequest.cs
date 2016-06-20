using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OneAndOne.POCO.Response;
using OneAndOne.POCO.Response.LoadBalancers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Requests.LoadBalancer
{
    public class AddLoadBalancerRuleRequest
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
        public LBRuleProtocol Protocol
        {
            get { return (LBRuleProtocol)Enum.Parse(typeof(LBRuleProtocol), protocol); }
            set
            {
                protocol = ((LBRuleProtocol)value).ToString();
            }
        }

        private int port_balancer;
        [JsonProperty(PropertyName = "port_balancer")]
        public int PortBalancer
        {
            get { return port_balancer; }
            set { port_balancer = value; }
        }
        private int port_server;
        [JsonProperty(PropertyName = "port_server")]
        public int PortServer
        {
            get { return port_server; }
            set { port_server = value; }
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
