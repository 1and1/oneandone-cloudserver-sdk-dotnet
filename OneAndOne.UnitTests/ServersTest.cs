using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using System.Collections.Generic;
using OneAndOne.POCO.Requests.Servers;

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
            var servers = client.Servers.GetServers(1, 3, "name");

            Assert.IsNotNull(servers);
            Assert.IsTrue(servers.Count > 0);
        }

        [TestMethod]
        public void GetAvailableServerFalvours()
        {
            var servers = client.Servers.GetAvailableFixedServers();

            Assert.IsNotNull(servers);
            Assert.IsTrue(servers.Count > 0);
        }

        [TestMethod]
        public void GetSingleFixedServerFalvours()
        {
            var all = client.Servers.GetAvailableFixedServers().FirstOrDefault();
            var result = client.Servers.GetFlavorInformation(all.id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.id);
        }

        [TestMethod]
        public void GetSingleServerData()
        {
            var servers = client.Servers.GetServers().FirstOrDefault();
            var result = client.Servers.GetSingleServer(servers.id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.id);
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
                },
                power_on = true
            });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.name);
            Assert.IsNotNull(result.hardware);
            Assert.IsNotNull(result.first_password);
        }

        [TestMethod]
        public void CreateServerWithFixedHardwareImage()
        {
            Random random = new Random();
            string randomName = "ServerTest" + random.Next(9999);
            var availabeFixedImage = client.Servers.GetAvailableFixedServers().FirstOrDefault();

            var result = client.Servers.CreateServer(new POCO.Requests.Servers.CreateServerRequest()
            {
                appliance_id = "B5F778B85C041347BCDCFC3172AB3F3C",
                name = randomName,
                description = "Example" + randomName,
                hardware = new POCO.Requests.Servers.HardwareReqeust()
                {
                    cores_per_processor = availabeFixedImage.hardware.cores_per_processor,
                    hdds = availabeFixedImage.hardware.hdds.Select(itm => new HddRequest()
                    {
                        is_main = itm.is_main,
                        size = itm.size

                    }).ToList(),
                    ram = availabeFixedImage.hardware.ram,
                    vcore = availabeFixedImage.hardware.vcore,
                },
                power_on = true
            });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.name);
            Assert.IsNotNull(result.hardware);
            Assert.IsNotNull(result.first_password);
        }


    }
}
