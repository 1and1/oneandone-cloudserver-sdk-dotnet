using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Respones.LoadBalancers
{
    //TODO: refactor properties
    public class LoadBalancerResponse
    {
        public string id { get; set; }
        public string name { get; set; }
        public string state { get; set; }
        public string creation_date { get; set; }
        public string description { get; set; }
        public string ip { get; set; }
        public string health_check_test { get; set; }
        public int health_check_interval { get; set; }
        public object health_check_path { get; set; }
        public object health_check_path_parser { get; set; }
        public bool persistence { get; set; }
        public int persistence_time { get; set; }
        public string method { get; set; }
        public List<Rule> rules { get; set; }
        public List<object> server_ips { get; set; }
        public string cloudpanel_id { get; set; }
    }

    public class Rule
    {
        public string id { get; set; }
        public string protocol { get; set; }
        public int port_balancer { get; set; }
        public int port_server { get; set; }
        public string source { get; set; }
    }
}
