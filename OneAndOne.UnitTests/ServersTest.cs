using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using System.Collections.Generic;
using OneAndOne.POCO.Requests.Servers;
using OneAndOne.POCO.Respones.Servers;

namespace OneAndOne.UnitTests
{
    [TestClass]
    public class ServersTest
    {
        static OneAndOneClient client = new OneAndOneClient();
        string randomName;

        public ServersTest()
        {
            Random random = new Random();
            randomName = "ServerTest" + random.Next(9999);
        }

        [TestMethod]
        public void CreateServer()
        {
            int vcore = 6;
            int CoresPerProcessor = 3;
            var servers = client.Servers.Get();
            if (servers.Any(ser => ser.Name == randomName))
            {
                return;
            }

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
            Assert.IsNotNull(result.Status.Percent);
        }

        [TestMethod]
        public void UpdateServer()
        {
            var server = client.Servers.Show(randomName);
            foreach (var item in client.Servers.Get())
            {
                if (item.Name.Contains("ServerTest") && (item.Status.State != ServerState.POWERED_OFF
                    && item.Status.State == ServerState.POWERED_OFF))
                {
                    server = item;
                    break;
                }
            }
            if (server != null)
            {
                var ranValue = new Random().Next(1, 100);
                if (!server.Name.Contains("Updated"))
                {
                    var result = client.Servers.Update(new UpdateServerRequest()
                        {
                            Description = "Updated desc" + ranValue,
                            Name = "Updated" + ranValue
                        }, server.Id);

                    Assert.IsNotNull(result);
                    Assert.IsNotNull(result.Name);
                    Assert.IsNotNull(result.Hardware);
                }
            }
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

        [TestMethod]
        public void DeleteServer()
        {
            ServerResponse serverToDelete = null;
            foreach (var item in client.Servers.Get())
            {
                if ((item.Name.Contains("ServerTest") || item.Name.Contains("Updated")) && (item.Status.State != ServerState.POWERED_OFF
                    && item.Status.State == ServerState.POWERED_ON))
                {
                    serverToDelete = item;
                    break;
                }
            }
            if (serverToDelete != null)
            {
                var result = client.Servers.Delete(serverToDelete.Id, false);
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Name);
                Assert.IsNotNull(result.Hardware);
                Assert.IsNotNull(result.FirstPassword);
            }
        }

        [TestMethod]
        public void DeleteAllTestServers()
        {
            var servers = client.Servers.Get().Where(ser => ser.Name.Contains("ServerTest") || ser.Name.Contains("Updated"));
            foreach (var item in servers)
            {
                var result = client.Servers.Delete(item.Id, false);
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Name);
                Assert.IsNotNull(result.Hardware);
            }


        }

        [TestMethod]
        public void GetServerStatus()
        {
            Random random = new Random();
            var servers = client.Servers.Get();
            var server = servers[random.Next(servers.Count - 1)];

            var result = client.Servers.GetStatus(server.Id);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.State);
        }

        [TestMethod]
        public void UpdateServerStatus()
        {
            Random random = new Random();
            var servers = client.Servers.Get();
            string newState = "REBOOT";
            var server = servers[random.Next(servers.Count - 1)];
            if (server.Status.State == ServerState.POWERED_OFF)
            {
                newState = "POWER_ON";
            }
            int i = 0;
            while (i < servers.Count && (server.Status.State == ServerState.POWERING_ON || server.Status.State == ServerState.REBOOTING))
            {
                server = servers[random.Next(i)];
            }

            var result = client.Servers.UpdateStatus(new UpdateStatusRequest()
            {
                Action = newState,
                Method = "SOFTWARE"
            }, server.Id);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Status);

        }


    }
}
