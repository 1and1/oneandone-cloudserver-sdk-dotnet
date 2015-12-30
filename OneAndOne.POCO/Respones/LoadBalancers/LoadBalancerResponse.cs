using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Respones.LoadBalancers
{
    public class LoadBalancerResponse
    {
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string state;

        public string State
        {
            get { return state; }
            set { state = value; }
        }
        private string creation_date;

        public string CreationDate
        {
            get { return creation_date; }
            set { creation_date = value; }
        }
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        private string ip;

        public string Ip
        {
            get { return ip; }
            set { ip = value; }
        }
        private string health_check_test;
        [JsonConverter(typeof(StringEnumConverter))]
        public HealthCheckTestTypes HealthCheckTest
        {
            get { return (HealthCheckTestTypes)Enum.Parse(typeof(HealthCheckTestTypes), health_check_test); }
            set
            {
                health_check_test = ((HealthCheckTestTypes)value).ToString();
            }
        }
        private int health_check_interval;

        public int HealthCheckInterval
        {
            get { return health_check_interval; }
            set { health_check_interval = value; }
        }
        private object health_check_path;

        public object Health_check_path
        {
            get { return health_check_path; }
            set { health_check_path = value; }
        }
        private string health_check_path_parser;

        public string HealthCheckPathParser
        {
            get { return health_check_path_parser; }
            set { health_check_path_parser = value; }
        }
        private bool persistence;

        public bool Persistence
        {
            get { return persistence; }
            set { persistence = value; }
        }
        private int persistence_time;

        public int PersistenceTime
        {
            get { return persistence_time; }
            set { persistence_time = value; }
        }
        private string method;
        [JsonConverter(typeof(StringEnumConverter))]
        public LoadBalancerMethod Method
        {
            get { return (LoadBalancerMethod)Enum.Parse(typeof(LoadBalancerMethod), method); }
            set
            {
                method = ((LoadBalancerMethod)value).ToString();
            }
        }
        private List<LoadBalancerRule> rules;

        public List<LoadBalancerRule> Rules
        {
            get { return rules; }
            set { rules = value; }
        }
        private List<IP> server_ips;

        public List<IP> ServerIps
        {
            get { return server_ips; }
            set { server_ips = value; }
        }
        private string cloudpanel_id;

        public string CloudpanelId
        {
            get { return cloudpanel_id; }
            set { cloudpanel_id = value; }
        }
    }
    public class LoadBalancerRule
    {
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string protocol;
        [JsonConverter(typeof(StringEnumConverter))]
        public LBRuleProtocol Protocol
        {
            get { return (LBRuleProtocol)Enum.Parse(typeof(LBRuleProtocol), protocol); }
            set
            {
                protocol = ((LBRuleProtocol)value).ToString();
            }
        }

        private int port_balancer;

        public int PortBalancer
        {
            get { return port_balancer; }
            set { port_balancer = value; }
        }
        private int port_server;

        public int PortServer
        {
            get { return port_server; }
            set { port_server = value; }
        }
        private string source;

        public string Source
        {
            get { return source; }
            set { source = value; }
        }
    }

    public enum LBRuleProtocol
    {
        TCP, UDP
    }
    public enum LoadBalancerMethod
    {
        ROUND_ROBIN, LEAST_CONNECTIONS
    }

    public enum HealthCheckTestTypes
    {
        TCP, HTTP, NONE
    }
}
