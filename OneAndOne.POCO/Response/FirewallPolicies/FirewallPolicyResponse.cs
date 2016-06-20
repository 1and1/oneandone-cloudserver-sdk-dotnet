using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Response
{
    public class FirewallPolicyResponse
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
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
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
        private int @default;

        public int Default
        {
            get { return @default; }
            set { @default = value; }
        }
        private List<FirewallRule> rules;

        public List<FirewallRule> Rules
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

    public class IP
    {
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        private string ip;

        public string Ip
        {
            get { return ip; }
            set { ip = value; }
        }
        private string server_name;

        public string ServerName
        {
            get { return server_name; }
            set { server_name = value; }
        }
    }

    public enum RuleProtocol
    {
        TCP, UDP, ICMP, AH, ESP, GRE
    }
    public class FirewallRule
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
