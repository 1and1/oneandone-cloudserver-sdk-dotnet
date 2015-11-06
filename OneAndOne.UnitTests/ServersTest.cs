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
        public void CreateServer()
        {
            Random random = new Random();
            string randomName = "ServerTest" + random.Next(9999);
            int vcore = new Random().Next(1, 16);
            int CoresPerProcessor = new Random().Next(1, vcore);
            var result = client.Servers.Create(new POCO.Requests.Servers.CreateServerRequest()
            {
                ApplianceId = "B5F778B85C041347BCDCFC3172AB3F3C",
                Name = randomName,
                Description = "Example" + randomName,
                Hardware = new POCO.Requests.Servers.HardwareReqeust()
                {
                    CoresPerProcessor = CoresPerProcessor,
                    Hdds = new List<POCO.Requests.Servers.HddRequest>()
                        {
                            {new POCO.Requests.Servers.HddRequest()
                            {
                                IsMain=true,
                                Size=20,
                            }}
                        },
                    Ram = new Random().Next(1, 128),
                    Vcore = vcore
                },
                PowerOn = true
            });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Name);
            Assert.IsNotNull(result.Hardware);
            Assert.IsNotNull(result.FirstPassword);
        }
        [TestMethod]
        public void GetServers()
        {
            var servers = client.Servers.Get();

            Assert.IsNotNull(servers);
            Assert.IsTrue(servers.Count > 0);
        }
        [TestMethod]
        public void GetServersWithPaging()
        {
            var servers = client.Servers.Get(1, 3, "name");

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
            var result = client.Servers.GetFlavorInformation(all.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void GetSingleServerData()
        {
            var servers = client.Servers.Get().FirstOrDefault();
            var result = client.Servers.Show(servers.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }



        [TestMethod]
        public void CreateServerWithFixedHardwareImage()
        {
            Random random = new Random();
            string randomName = "ServerTest" + random.Next(9999);
            var availabeFixedImage = client.Servers.GetAvailableFixedServers().FirstOrDefault();

            var result = client.Servers.Create(new POCO.Requests.Servers.CreateServerRequest()
            {
                ApplianceId = "B5F778B85C041347BCDCFC3172AB3F3C",
                Name = randomName,
                Description = "Example" + randomName,
                Hardware = new POCO.Requests.Servers.HardwareReqeust()
                {
                    CoresPerProcessor = availabeFixedImage.Hardware.CoresPerProcessor,
                    Hdds = availabeFixedImage.Hardware.Hdds.Select(itm => new HddRequest()
                    {
                        IsMain = itm.IsMain,
                        Size = itm.Size

                    }).ToList(),
                    Ram = availabeFixedImage.Hardware.Ram,
                    Vcore = availabeFixedImage.Hardware.Vcore,
                },
                PowerOn = true
            });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Name);
            Assert.IsNotNull(result.Hardware);
            Assert.IsNotNull(result.FirstPassword);
        }


    }
}
