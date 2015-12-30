﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Requests.FirewallPolicies
{
    public class AssignFirewallServerIPRequest
    {
        private List<string> server_ips;
        [JsonProperty(PropertyName = "server_ips")]
        public List<string> ServerIps
        {
            get { return server_ips; }
            set { server_ips = value; }
        }
    }

}
