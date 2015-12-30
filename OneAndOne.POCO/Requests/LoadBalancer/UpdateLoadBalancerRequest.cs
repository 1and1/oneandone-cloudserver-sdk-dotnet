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
    public class UpdateLoadBalancerRequest
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
        private string health_check_test;
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(PropertyName = "health_check_test")]
        /// <summary>
        /// Required: Type of the health check. At the moment, HTTP is not allowed.
        /// </summary>
        /// 
        public HealthCheckTestTypes HealthCheckTest
        {
            get { return (HealthCheckTestTypes)Enum.Parse(typeof(HealthCheckTestTypes), health_check_test); }
            set
            {
                health_check_test = ((HealthCheckTestTypes)value).ToString();
            }
        }
        private int health_check_interval;
        [JsonProperty(PropertyName = "health_check_interval")]
        /// <summary>
        /// Required:Health check period in seconds"minimum": "5", "maximum": "300", "multipleOf": "1",
        /// </summary>
        /// 
        public int HealthCheckInterval
        {
            get { return health_check_interval; }
            set { health_check_interval = value; }
        }

        private bool persistence;
        /// <summary>
        /// Required:Persistence time in seconds. Required if persistence is enabled."minimum": "30","maximum": "1200", "multipleOf": "1",
        /// </summary>
        /// 
        [JsonProperty(PropertyName = "persistence")]
        public bool Persistence
        {
            get { return persistence; }
            set { persistence = value; }
        }
        private int persistence_time;

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

        private string health_check_path;
        [JsonProperty(PropertyName = "health_check_path")]
        /// <summary>
        /// Url to call for checking. Required for HTTP health check.
        /// </summary>
        /// 
        public string HealthCheckPath
        {
            get { return health_check_path; }
            set { health_check_path = value; }
        }

        private string health_check_path_parser;
        [JsonProperty(PropertyName = "health_check_parse")]
        /// <summary>
        /// Regular expression to check. Required for HTTP health check.
        /// </summary>
        /// 
        public string HealthCheckPathParse
        {
            get { return health_check_path_parser; }
            set { health_check_path_parser = value; }
        }
    }
}
