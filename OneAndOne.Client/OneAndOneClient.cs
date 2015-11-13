using OneAndOne.Client.Endpoints.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.Client
{
    public class OneAndOneClient
    {

        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static OneAndOneClient Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new OneAndOneClient();
                    }
                }

                return instance;
            }
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
                return new Servers();
            }
        }

        /// <summary>
        /// Servers hardware client
        /// </summary>
        public Hardware ServersHardware
        {
            get
            {
                return new Hardware();
            }
        }

        /// <summary>
        /// Servers HDD client
        /// </summary>
        public HardDiskDrives ServerHdds
        {
            get
            {
                return new HardDiskDrives();
            }
        }

        /// <summary>
        /// Servers Image client
        /// </summary>
        public Image ServerImage
        {
            get
            {
                return new Image();
            }
        }

        /// <summary>
        /// Servers IP client
        /// </summary>
        public IPs ServerIps
        {
            get
            {
                return new IPs();
            }
        }

        /// <summary>
        /// Load balancer client
        /// </summary>
        public OneAndOne.Client.Endpoints.LoadBalancers.LoadBalancer LoadBalancers
        {
            get
            {
                return new OneAndOne.Client.Endpoints.LoadBalancers.LoadBalancer();
            }
        }

        /// <summary>
        /// DVD ISO client
        /// </summary>
        public OneAndOne.Client.Endpoints.DVDs.DVDs DVDs
        {
            get
            {
                return new OneAndOne.Client.Endpoints.DVDs.DVDs();
            }
        }

        /// <summary>
        /// DVD ISO client
        /// </summary>
        public OneAndOne.Client.Endpoints.PrivateNetworks.PrivateNetworks PrivateNetworks
        {
            get
            {
                return new OneAndOne.Client.Endpoints.PrivateNetworks.PrivateNetworks();
            }
        }

        /// <summary>
        /// Images client
        /// </summary>
        public OneAndOne.Client.Endpoints.Images.Images Images
        {
            get
            {
                return new OneAndOne.Client.Endpoints.Images.Images();
            }
        }

        /// <summary>
        /// SharedStorages client
        /// </summary>
        public OneAndOne.Client.Endpoints.SharedStorages.SharedStorages SharedStorages
        {
            get
            {
                return new OneAndOne.Client.Endpoints.SharedStorages.SharedStorages();
            }
        }

        /// <summary>
        /// FirewallPolicies client
        /// </summary>
        public OneAndOne.Client.Endpoints.FirewallPolicies.FirewallPolicies FirewallPolicies
        {
            get
            {
                return new OneAndOne.Client.Endpoints.FirewallPolicies.FirewallPolicies();
            }
        }
    }
}
