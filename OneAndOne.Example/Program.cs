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
            var serversResult = client.Servers.Get(2, 3);
        }

        static void CreateServers()
        {

            var result = client.Servers.Create(new POCO.Requests.Servers.CreateServerRequest()
                {
                    ApplianceId = "B5F778B85C041347BCDCFC3172AB3F3C",
                    Name = "ServerTest001",
                    Description = "Example server",
                    Hardware = new POCO.Requests.Servers.HardwareReqeust()
                    {
                        CoresPerProcessor = 1,
                        Hdds = new List<POCO.Requests.Servers.HddRequest>()
                        {
                            {new POCO.Requests.Servers.HddRequest()
                            {
                                IsMain=true,
                                Size=20,
                            }}
                        },
                        Ram = 2,
                        Vcore = 1

                    }
                });
        }
    }
}
