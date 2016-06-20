using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Response.LoadBalancers
{
    public class LoadBalancerRulesResponse
    {
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string protocol;
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

        public int PortFrom
        {
            get { return port_from; }
            set { port_from = value; }
        }
        private int port_to;

        public int PortTo
        {
            get { return port_to; }
            set { port_to = value; }
        }
        private string source;

        public string Source
        {
            get { return source; }
            set { source = value; }
        }
    }
}
