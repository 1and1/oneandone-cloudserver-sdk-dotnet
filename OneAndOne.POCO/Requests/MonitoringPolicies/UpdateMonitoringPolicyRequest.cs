using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Requests.MonitoringPolicies
{
    public class UpdateMonitoringPolicyRequest
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
        private string email;
        [JsonProperty(PropertyName = "email")]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        private Thresholds thresholds;
        [JsonProperty(PropertyName = "thresholds")]
        public Thresholds Thresholds
        {
            get { return thresholds; }
            set { thresholds = value; }
        }
    }
}

