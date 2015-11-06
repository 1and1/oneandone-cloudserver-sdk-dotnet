using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using System.Collections.Generic;

namespace OneAndOne.UnitTests
{
    [TestClass]
    public class ServersTest
    {
        static OneAndOneClient client = new OneAndOneClient();
        [TestMethod]
        public void GetServers()
        {
            var servers = client.Servers.GetServers();

            Assert.IsNotNull(servers);
            Assert.IsTrue(servers.Count > 0);
        }
        [TestMethod]
        public void GetServersWithPaging()
        {
            var servers = client.Servers.GetServers(1, 3, "creation_date");

            Assert.IsNotNull(servers);
            Assert.IsTrue(servers.Count > 0);
        }

        [TestMethod]
        public void CreateServer()
        {
            Random random = new Random();
            string randomName = "ServerTest" + random.Next(9999);
            var result = client.Servers.CreateServer(new POCO.Requests.Servers.CreateServerRequest()
            {
                appliance_id = "B5F778B85C041347BCDCFC3172AB3F3C",
                name = randomName,
                description = "Example" + randomName,
                hardware = new POCO.Requests.Servers.HardwareReqeust()
                {
                    cores_per_processor = new Random().Next(1, 16),
                    hdds = new List<POCO.Requests.Servers.HddRequest>()
                        {
                            {new POCO.Requests.Servers.HddRequest()
                            {
                                is_main=true,
                                size=20,
                            }}
                        },
                    ram = new Random().Next(1, 128),
                    vcore = new Random().Next(1, 16)

                }
            });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.name);
            Assert.IsNotNull(result.hardware);
            Assert.IsNotNull(result.first_password);
        }


    }
}
