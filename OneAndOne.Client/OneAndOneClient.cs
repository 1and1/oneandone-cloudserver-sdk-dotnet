#define NEW_ENDPOINT
using OneAndOne.Client.Endpoints;
using OneAndOne.Client.Endpoints.Common;
using OneAndOne.Client.Endpoints.DataCenter;
using OneAndOne.Client.Endpoints.DVDs;
using OneAndOne.Client.Endpoints.FirewallPolicies;
using OneAndOne.Client.Endpoints.Images;
using OneAndOne.Client.Endpoints.LoadBalancers;
using OneAndOne.Client.Endpoints.Logs;
using OneAndOne.Client.Endpoints.MonitoringCenter;
using OneAndOne.Client.Endpoints.MonitoringPolicies;
using OneAndOne.Client.Endpoints.PrivateNetworks;
using OneAndOne.Client.Endpoints.PublicIPs;
using OneAndOne.Client.Endpoints.RecoveryAppliances;
using OneAndOne.Client.Endpoints.Roles;
using OneAndOne.Client.Endpoints.ServerAppliances;
using OneAndOne.Client.Endpoints.Servers;
using OneAndOne.Client.Endpoints.SharedStorages;
using OneAndOne.Client.Endpoints.Usages;
using OneAndOne.Client.Endpoints.Users;
using OneAndOne.Client.Endpoints.Vpn;
using OneAndOne.Client.Endpoints.SshKeys;
using OneAndOne.Client.Endpoints.BlockStorages;
using OneAndOne.Client.RESTHelpers;
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

        /**
         * This endpoint was changed at some point in time and
         * was never updated. I've preserved the original URL for
         * historical purposes only. Feel free to remove it and 
         * replace it out-right if you desire ;).
         */
#if NEW_ENDPOINT
        static string Endpoint = "https://cloudpanel-api.ionos.com/v1";
#else
        static String Endpoint = "https://cloudpanel-api.1and1.com/v1";
#endif
        static Configuration configuration;
        private OneAndOneClient(Configuration config)
        {
            configuration = config;
            if (config.ApiUrl == null)
            {
                config.ApiUrl = Endpoint;
            }
        }

        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static OneAndOneClient Instance(Configuration config)
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new OneAndOneClient(config);
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
                return new Servers(configuration.ApiUrl, configuration.ApiKey);
            }
        }

        /// <summary>
        /// Servers hardware client
        /// </summary>
        public ServersHardware ServersHardware
        {
            get
            {
                return new ServersHardware(configuration.ApiUrl, configuration.ApiKey);
            }
        }

        /// <summary>
        /// Servers HDD client
        /// </summary>
        public ServerHdds ServerHdds
        {
            get
            {
                return new ServerHdds(configuration.ApiUrl, configuration.ApiKey);
            }
        }

        /// <summary>
        /// Servers Image client
        /// </summary>
        public ServerImage ServerImage
        {
            get
            {
                return new ServerImage(configuration.ApiUrl, configuration.ApiKey);
            }
        }

        /// <summary>
        /// Servers IP client
        /// </summary>
        public ServerIps ServerIps
        {
            get
            {
                return new ServerIps(configuration.ApiUrl, configuration.ApiKey);
            }
        }

        /// <summary>
        /// Load balancer client
        /// </summary>
        public LoadBalancer LoadBalancer
        {
            get
            {
                return new LoadBalancer(configuration.ApiUrl, configuration.ApiKey);
            }
        }

        /// <summary>
        /// DVD ISO client
        /// </summary>
        public DVDs DVDs
        {
            get
            {
                return new DVDs(configuration.ApiUrl, configuration.ApiKey);
            }
        }

        /// <summary>
        /// DVD ISO client
        /// </summary>
        public PrivateNetworks PrivateNetworks
        {
            get
            {
                return new PrivateNetworks(configuration.ApiUrl, configuration.ApiKey);
            }
        }

        /// <summary>
        /// Images client
        /// </summary>
        public Images Images
        {
            get
            {
                return new Images(configuration.ApiUrl, configuration.ApiKey);
            }
        }

        /// <summary>
        /// SharedStorages client
        /// </summary>
        public SharedStorages SharedStorages
        {
            get
            {
                return new SharedStorages(configuration.ApiUrl, configuration.ApiKey);
            }
        }

        /// <summary>
        /// FirewallPolicies client
        /// </summary>
        public FirewallPolicies FirewallPolicies
        {
            get
            {
                return new FirewallPolicies(configuration.ApiUrl, configuration.ApiKey);
            }
        }

        /// <summary>
        /// Public IPs client
        /// </summary>
        public PublicIPs PublicIPs
        {
            get
            {
                return new PublicIPs(configuration.ApiUrl, configuration.ApiKey);
            }
        }

        /// <summary>
        /// Monitoring Center client
        /// </summary>
        public MonitoringCenter MonitoringCenter
        {
            get
            {
                return new MonitoringCenter(configuration.ApiUrl, configuration.ApiKey);
            }
        }

        /// <summary>
        /// Monitoring policies client
        /// </summary>
        public MonitoringPolicies MonitoringPolicies
        {
            get
            {
                return new MonitoringPolicies(configuration.ApiUrl, configuration.ApiKey);
            }
        }

        /// <summary>
        /// Monitoring policy ports client
        /// </summary>
        public MonitoringPoliciesPorts MonitoringPoliciesPorts
        {
            get
            {
                return new MonitoringPoliciesPorts(configuration.ApiUrl, configuration.ApiKey);
            }
        }

        /// <summary>
        /// Monitoring policy processes client
        /// </summary>
        public MonitoringPoliciesProcesses MonitoringPoliciesProcesses
        {
            get
            {
                return new MonitoringPoliciesProcesses(configuration.ApiUrl, configuration.ApiKey);
            }
        }

        /// <summary>
        /// Monitoring policy servers client
        /// </summary>
        public MonitoringPoliciesServers MonitoringPoliciesServers
        {
            get
            {
                return new MonitoringPoliciesServers(configuration.ApiUrl, configuration.ApiKey);
            }
        }

        /// <summary>
        /// Logs client
        /// </summary>
        public Logs Logs
        {
            get
            {
                return new Logs(configuration.ApiUrl, configuration.ApiKey);
            }
        }

        /// <summary>
        /// Users client
        /// </summary>
        public Users Users
        {
            get
            {
                return new Users(configuration.ApiUrl, configuration.ApiKey);
            }
        }

        /// <summary>
        /// UserAPI client
        /// </summary>
        public UserAPI UserAPI
        {
            get
            {
                return new UserAPI(configuration.ApiUrl, configuration.ApiKey);
            }
        }

        /// <summary>
        /// Usages client
        /// </summary>
        public Usages Usages
        {
            get
            {
                return new Usages(configuration.ApiUrl, configuration.ApiKey);
            }
        }

        /// <summary>
        /// ServerAppliances client
        /// </summary>
        public ServerAppliances ServerAppliances
        {
            get
            {
                return new ServerAppliances(configuration.ApiUrl, configuration.ApiKey);
            }
        }

        /// <summary>
        /// RecoveryAppliances client
        /// </summary>
        public RecoveryAppliances RecoveryAppliances
        {
            get
            {
                return new RecoveryAppliances(configuration.ApiUrl, configuration.ApiKey);
            }
        }

        /// <summary>
        /// DataCenters client
        /// </summary>
        public DataCenters DataCenters
        {
            get
            {
                return new DataCenters(configuration.ApiUrl, configuration.ApiKey);
            }
        }

        /// <summary>
        /// Vpn client
        /// </summary>
        public Vpn Vpn
        {
            get
            {
                return new Vpn(configuration.ApiUrl, configuration.ApiKey);
            }
        }

        /// <summary>
        /// Role client
        /// </summary>
        public Roles Roles
        {
            get
            {
                return new Roles(configuration.ApiUrl, configuration.ApiKey);
            }
        }

        /// <summary>
        /// Common operations like ping and pricing client
        /// </summary>
        public Common Common
        {
            get
            {
                return new Common(configuration.ApiUrl, configuration.ApiKey);
            }
        }

        /// <summary>
        /// Vpn client
        /// </summary>
        public SshKeys SshKeys
        {
            get
            {
                return new SshKeys(configuration.ApiUrl, configuration.ApiKey);
            }
        }

        /// <summary>
        /// BlockStorages client
        /// </summary>
        public BlockStorages BlockStorages
        {
            get
            {
                return new BlockStorages(configuration.ApiUrl, configuration.ApiKey);
            }
        }
    }
}
