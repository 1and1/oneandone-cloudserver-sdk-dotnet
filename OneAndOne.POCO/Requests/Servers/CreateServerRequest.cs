using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OneAndOne.POCO.Requests.Servers
{
    /// <summary>
    /// Adds a new server.
    /// </summary>
    /// 

    public class CreateServerRequest
    {
        /// <summary>
        /// Required: Name of the server.
        /// </summary>
        /// 

        private string name;
        [JsonProperty(PropertyName = "name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Type of the server ["cloud", "baremetal"].
        /// </summary>
        /// 
        private string server_type="cloud";
        [JsonProperty(PropertyName = "server_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ServerType ServerType
        {
            get { return (ServerType)Enum.Parse(typeof(ServerType), server_type); }
            set
            {
                server_type = ((ServerType)value).ToString("F");
            }
        }

        //public string name;
        /// <summary>
        /// Required: description of your servers.
        /// </summary>
        /// 
        private string description;
        [JsonProperty(PropertyName = "description")]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        /// <summary>
        /// Required: Hardware features of the server. Choose your resources using fixed_instance_size_id or customizing your hardware.
        /// </summary>
        /// 
        private HardwareRequest hardware;
        [JsonProperty(PropertyName = "hardware")]
        public HardwareRequest Hardware
        {
            get { return hardware; }
            set { hardware = value; }
        }

        /// <summary>
        /// Required: Image will be installed on server
        /// </summary>
        /// 
        private string appliance_id;
        [JsonProperty(PropertyName = "appliance_id")]
        public string ApplianceId
        {
            get { return appliance_id; }
            set { appliance_id = value; }
        }

        /// <summary>
        /// Password of the server. Password must contain more than 8 characters using uppercase letters, numbers and other special symbols. minLength: 8,maxLength: 64.
        /// </summary>
        /// 
        private string password;
        [JsonProperty(PropertyName = "password")]
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        /// <summary>
        ///NOT REQUIRED: ID of the region where the server will be created 
        /// </summary>
        /// 
        private string region_id;
        [JsonProperty(PropertyName = "region_id")]
        public string RegionId
        {
            get { return region_id; }
            set { region_id = value; }
        }

        /// <summary>
        /// Power on server after creation
        /// </summary>
        /// 
        private bool power_on;
        [JsonProperty(PropertyName = "power_on")]
        public bool PowerOn
        {
            get { return power_on; }
            set { power_on = value; }
        }

        /// <summary>
        /// Firewall policy's ID
        /// </summary>
        /// 
        private string firewall_policy_id;
        [JsonProperty(PropertyName = "firewall_policy_id")]
        public string FirewallPolicyId
        {
            get { return firewall_policy_id; }
            set { firewall_policy_id = value; }
        }

        /// <summary>
        /// IP's ID
        /// </summary>
        /// 
        private string ip_id;
        [JsonProperty(PropertyName = "ip_id")]
        public string IpId
        {
            get { return ip_id; }
            set { ip_id = value; }
        }

        /// <summary>
        /// Load balancer's ID
        /// </summary>
        /// 
        private string loadr_balancer_id;
        [JsonProperty(PropertyName = "loadr_balancer_id")]
        public string LoadrBalancerId
        {
            get { return loadr_balancer_id; }
            set { loadr_balancer_id = value; }
        }

        /// <summary>
        /// Monitoring policy's ID
        /// </summary>
        /// 
        private string monitoring_policy_id;
        [JsonProperty(PropertyName = "monitoring_policy_id")]
        public string MonitoringPolicyId
        {
            get { return monitoring_policy_id; }
            set { monitoring_policy_id = value; }
        }
    }
}
