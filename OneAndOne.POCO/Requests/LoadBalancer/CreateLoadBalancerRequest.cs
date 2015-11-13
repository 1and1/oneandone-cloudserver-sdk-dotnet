using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OneAndOne.POCO.Respones.LoadBalancers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Requests.LoadBalancer
{
    public class CreateLoadBalancerRequest
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
        /// <summary>
        /// Required: Type of the health check. At the moment, HTTP is not allowed.
        /// </summary>
        /// 
        private string health_check_test;
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(PropertyName = "health_check_test")]
        public HealthCheckTestTypes HealthCheckTest
        {
            get { return (HealthCheckTestTypes)Enum.Parse(typeof(HealthCheckTestTypes), health_check_test); }
            set
            {
                health_check_test = ((HealthCheckTestTypes)value).ToString();
            }
        }

        /// <summary>
        /// Required:Health check period in seconds"minimum": "5", "maximum": "300", "multipleOf": "1",
        /// </summary>
        /// 
        private int health_check_interval;
        [JsonProperty(PropertyName = "health_check_interval")]
        public int HealthCheckInterval
        {
            get { return health_check_interval; }
            set { health_check_interval = value; }
        }
        private string health_check_path;
        /// <summary>
        /// Not Required: Url to call for cheking. Required for HTTP health check."maxLength": 1000.
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "health_check_path")]
        public string HealthCheckPath
        {
            get { return health_check_path; }
            set { health_check_path = value; }
        }
        private string health_check_path_parser;
        /// <summary>
        /// Not Required: Regular expression to check. Required for HTTP health check."maxLength": 64,
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "health_check_path_parser")]
        public string HealthCheckPathParser
        {
            get { return health_check_path_parser; }
            set { health_check_path_parser = value; }
        }

        private bool persistence;

        [JsonProperty(PropertyName = "persistence")]
        public bool Persistence
        {
            get { return persistence; }
            set { persistence = value; }
        }
        private int persistence_time;
        /// <summary>
        /// Required:Persistence time in seconds. Required if persistence is enabled."minimum": "30","maximum": "1200", "multipleOf": "1",
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "persistence_time")]
        public int PersistenceTime
        {
            get { return persistence_time; }
            set { persistence_time = value; }
        }

        private string method;
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(PropertyName = "method")]
        /// <summary>
        /// Required:Balancing procedure
        /// </summary>
        /// 
        public LoadBalancerMethod Method
        {
            get { return (LoadBalancerMethod)Enum.Parse(typeof(LoadBalancerMethod), method); }
            set
            {
                method = ((LoadBalancerMethod)value).ToString();
            }
        }
        private List<LoadBalancerRuleRequest> rules;
        [JsonProperty(PropertyName = "rules")]
        public List<LoadBalancerRuleRequest> Rules
        {
            get { return rules; }
            set { rules = value; }
        }
    }
    public class LoadBalancerRuleRequest
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
