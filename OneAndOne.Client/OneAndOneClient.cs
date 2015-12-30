using OneAndOne.Client.Endpoints;
using OneAndOne.Client.Endpoints.DVDs;
using OneAndOne.Client.Endpoints.FirewallPolicies;
using OneAndOne.Client.Endpoints.Images;
using OneAndOne.Client.Endpoints.LoadBalancers;
using OneAndOne.Client.Endpoints.Logs;
using OneAndOne.Client.Endpoints.MonitoringCenter;
using OneAndOne.Client.Endpoints.MonitoringPolicies;
using OneAndOne.Client.Endpoints.PrivateNetworks;
using OneAndOne.Client.Endpoints.PublicIPs;
using OneAndOne.Client.Endpoints.ServerAppliances;
using OneAndOne.Client.Endpoints.Servers;
using OneAndOne.Client.Endpoints.SharedStorages;
using OneAndOne.Client.Endpoints.Usages;
using OneAndOne.Client.Endpoints.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.Client
{
    public class OneAndOneClient
    {

        object[] args = new object[2];
        string AssemblyName;
        private OneAndOneClient(string apiUrl = null, string apiKey = null)
        {
            args[0] = apiUrl;
            args[1] = apiKey;
            AssemblyName = typeof(OneAndOneClient).Assembly.GetName().Name;
        }

        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static OneAndOneClient Instance(string apiUrl = null, string apiKey = null)
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new OneAndOneClient(apiUrl, apiKey);
                }
            }

            return instance;
        }
       

        /// <summary>
        /// singleton instance.
        /// </summary>
        private static volatile OneAndOneClient instance;

        /// <summary>
        /// lock
        /// </summary>
        private static object syncRoot = new Object();

        /// <summary>
        /// Servers client
        /// </summary>
        public Servers Servers
        {
            get
            {
                return new Servers(args[0], args[1]);
            }
        }

        /// <summary>
        /// Servers hardware client
        /// </summary>
        public ServersHardware ServersHardware
        {
            get
            {
                return new ServersHardware(args[0], args[1]);
            }
        }

        /// <summary>
        /// Servers HDD client
        /// </summary>
        public ServerHdds ServerHdds
        {
            get
            {
                return new ServerHdds(args[0], args[1]);
            }
        }

        /// <summary>
        /// Servers Image client
        /// </summary>
        public ServerImage ServerImage
        {
            get
            {
                return new ServerImage(args[0], args[1]);
            }
        }

        /// <summary>
        /// Servers IP client
        /// </summary>
        public ServerIps ServerIps
        {
            get
            {
                return new ServerIps(args[0], args[1]);
            }
        }

        /// <summary>
        /// Load balancer client
        /// </summary>
        public LoadBalancer LoadBalancer
        {
            get
            {
                return new LoadBalancer(args[0], args[1]);
            }
        }

        /// <summary>
        /// DVD ISO client
        /// </summary>
        public DVDs DVDs
        {
            get
            {
                return new DVDs(args[0], args[1]);
            }
        }

        /// <summary>
        /// DVD ISO client
        /// </summary>
        public PrivateNetworks PrivateNetworks
        {
            get
            {
                return new PrivateNetworks(args[0], args[1]);
            }
        }

        /// <summary>
        /// Images client
        /// </summary>
        public Images Images
        {
            get
            {
                return new Images(args[0], args[1]);
            }
        }

        /// <summary>
        /// SharedStorages client
        /// </summary>
        public SharedStorages SharedStorages
        {
            get
            {
                return new SharedStorages(args[0], args[1]);
            }
        }

        /// <summary>
        /// FirewallPolicies client
        /// </summary>
        public FirewallPolicies FirewallPolicies
        {
            get
            {
                return new FirewallPolicies(args[0], args[1]);
            }
        }

        /// <summary>
        /// Public IPs client
        /// </summary>
        public PublicIPs PublicIPs
        {
            get
            {
                return new PublicIPs(args[0], args[1]);
            }
        }

        /// <summary>
        /// Monitoring Center client
        /// </summary>
        public MonitoringCenter MonitoringCenter
        {
            get
            {
                return new MonitoringCenter(args[0], args[1]);
            }
        }

        /// <summary>
        /// Monitoring policies client
        /// </summary>
        public MonitoringPolicies MonitoringPolicies
        {
            get
            {
                return new MonitoringPolicies(args[0], args[1]);
            }
        }

        /// <summary>
        /// Monitoring policy ports client
        /// </summary>
        public MonitoringPoliciesPorts MonitoringPoliciesPorts
        {
            get
            {
                return new MonitoringPoliciesPorts(args[0], args[1]);
            }
        }

        /// <summary>
        /// Monitoring policy processes client
        /// </summary>
        public MonitoringPoliciesProcesses MonitoringPoliciesProcesses
        {
            get
            {
                return new MonitoringPoliciesProcesses(args[0], args[1]);
            }
        }

        /// <summary>
        /// Monitoring policy servers client
        /// </summary>
        public MonitoringPoliciesServers MonitoringPoliciesServers
        {
            get
            {
                return new MonitoringPoliciesServers(args[0], args[1]);
            }
        }

        /// <summary>
        /// Logs client
        /// </summary>
        public Logs Logs
        {
            get
            {
                return new Logs(args[0], args[1]);
            }
        }

        /// <summary>
        /// Users client
        /// </summary>
        public Users Users
        {
            get
            {
                return new Users(args[0], args[1]);
            }
        }

        /// <summary>
        /// UserAPI client
        /// </summary>
        public UserAPI UserAPI
        {
            get
            {
                return new UserAPI(args[0], args[1]);
            }
        }

        /// <summary>
        /// Usages client
        /// </summary>
        public Usages Usages
        {
            get
            {
                return new Usages(args[0], args[1]);
            }
        }

        /// <summary>
        /// ServerAppliances client
        /// </summary>
        public ServerAppliances ServerAppliances
        {
            get
            {
                return new ServerAppliances(args[0], args[1]);
            }
        }
    }
}
