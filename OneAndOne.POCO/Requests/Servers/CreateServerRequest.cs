using OneAndOne.POCO.Respones.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Requests.Servers
{
    /// <summary>
    /// Adds a new server.
    /// </summary>
    public class CreateServerRequest
    {
        /// <summary>
        /// Required: Name of the server.
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Required: description of your servers.
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// Required: Hardware features of the server. Choose your resources using fixed_instance_size_id or customizing your hardware.
        /// </summary>
        public HardwareReqeust hardware { get; set; }
        /// <summary>
        /// Required: Image will be installed on server
        /// </summary>
        public string appliance_id { get; set; }

        /// <summary>
        /// Password of the server. Password must contain more than 8 characters using uppercase letters, numbers and other special symbols. minLength: 8,maxLength: 64.
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// Power on server after creation
        /// </summary>
        public bool power_on { get; set; }
        /// <summary>
        /// Firewall policy's ID
        /// </summary>
        public string firewall_policy_id { get; set; }
        /// <summary>
        /// IP's ID
        /// </summary>
        public string ip_id { get; set; }
        /// <summary>
        /// Load balancer's ID
        /// </summary>
        public string load_balancer_id { get; set; }
        /// <summary>
        /// Monitoring policy's ID
        /// </summary>
        public string monitoring_policy_id { get; set; }


    }
}
