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
        public ServerHardware ServersHardware
        {
            get
            {
                return new ServerHardware();
            }
        }

        public ServerHardwareHdd ServerHdds
        {
            get
            {
                return new ServerHardwareHdd();
            }
        }
    }
}
