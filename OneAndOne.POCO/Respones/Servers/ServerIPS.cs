using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Respones.Servers
{
    public class IpAddress
    {
        private string id;
        [JsonProperty(PropertyName = "id")]

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string ip;
        [JsonProperty(PropertyName = "ip")]

        public string Ip
        {
            get { return ip; }
            set { ip = value; }
        }


        private List<LoadBalancers> load_balancers;
        [JsonProperty(PropertyName = "load_balancers")]

        public List<LoadBalancers> LoadBalancers
        {
            get { return load_balancers; }
            set { load_balancers = value; }
        }
        private List<FirewallPolicyResponse> firewall_policy;
        [JsonProperty(PropertyName = "firewall_policy")]

        public List<FirewallPolicyResponse> FirewallPolicy
        {
            get { return firewall_policy; }
            set { firewall_policy = value; }
        }

        private string reverse_dns;
        [JsonProperty(PropertyName = "reverse_dns")]

        public string ReverseDns
        {
            get { return reverse_dns; }
            set { reverse_dns = value; }
        }

        private string type;
        [JsonProperty(PropertyName = "type")]

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
    }

    public class DVDReponseIpAddress
    {
        private string id;
        [JsonProperty(PropertyName = "id")]

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string ip;
        [JsonProperty(PropertyName = "ip")]

        public string Ip
        {
            get { return ip; }
            set { ip = value; }
        }


        private List<LoadBalancers> load_balancers;
        [JsonProperty(PropertyName = "load_balancers")]

        public List<LoadBalancers> LoadBalancers
        {
            get { return load_balancers; }
            set { load_balancers = value; }
        }
        private FirewallPolicyResponse firewall_policy;
        [JsonProperty(PropertyName = "firewall_policy")]

        public FirewallPolicyResponse FirewallPolicy
        {
            get { return firewall_policy; }
            set { firewall_policy = value; }
        }

        private string reverse_dns;
        [JsonProperty(PropertyName = "reverse_dns")]

        public string ReverseDns
        {
            get { return reverse_dns; }
            set { reverse_dns = value; }
        }

        private string type;
        [JsonProperty(PropertyName = "type")]

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
