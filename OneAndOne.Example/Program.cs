using OneAndOne.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.Example
{
    public class Program
    {
        static OneAndOneClient client = new OneAndOneClient();
        static void Main(string[] args)
        {
            //GetServers();
            CreateServers();

        }

        static void GetServers()
        {
            var serversResult = client.Servers.GetServers(2, 3);
        }

        static void CreateServers()
        {

            var result = client.Servers.CreateServer(new POCO.Requests.Servers.CreateServerRequest()
                {
                    appliance_id = "B5F778B85C041347BCDCFC3172AB3F3C",
                    name = "ServerTest001",
                    description = "Example server",
                    hardware = new POCO.Requests.Servers.HardwareReqeust()
                    {
                        cores_per_processor = 1,
                        hdds = new List<POCO.Requests.Servers.HddRequest>()
                        {
                            {new POCO.Requests.Servers.HddRequest()
                            {
                                is_main=true,
                                size=20,
                            }}
                        },
                        ram = 2,
                        vcore = 1

                    }
                });
        }
    }
}
