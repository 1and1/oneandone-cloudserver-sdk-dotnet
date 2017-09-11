using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using System.Collections.Generic;
using OneAndOne.POCO.Requests.Servers;
using OneAndOne.POCO.Response.Servers;
using System.Threading;
using OneAndOne.POCO;
using OneAndOne.POCO.Response.ServerAppliances;
using OneAndOne.POCO.Response.PrivateNetworks;

namespace OneAndOne.UnitTests
{
    [TestClass]
    public class ServersTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance(Config.Configuration);
        static ServerResponse server = null;
        static ServerResponse clone = null;
        static ServerResponse flavoredServer = null;
        static POCO.Response.ServerAppliances.ServerAppliancesResponse appliance = null;
        static PrivateNetworksResponse pn;
        static List<string> serverIds = new List<string>();

        [ClassInitialize]
        static public void ServerHardwareInit(TestContext context)
        {
            int vcore = 4;
            int CoresPerProcessor = 2;
            var appliances = client.ServerAppliances.Get(null, null, null, "coreos", null);
            if (appliances == null || appliances.Count() == 0)
            {
                appliance = client.ServerAppliances.Get().FirstOrDefault();
            }
            else
            {
                appliance = appliances.FirstOrDefault();
            }
            var result = client.Servers.Create(new POCO.Requests.Servers.CreateServerRequest()
            {
                ApplianceId = appliance != null ? appliance.Id : null,
                Name = "main1 server test .net",
                Description = "desc",
                Hardware = new POCO.Requests.Servers.HardwareRequest()
                {
                    CoresPerProcessor = CoresPerProcessor,
                    Hdds = new List<POCO.Requests.Servers.HddRequest>()
                        {
                            {new POCO.Requests.Servers.HddRequest()
                            {
                                IsMain=true,
                                Size=20,
                            }},
                            {new POCO.Requests.Servers.HddRequest()
                            {
                                IsMain=false,
                                Size=20,
                            }}
                        },
                    Ram = 4,
                    Vcore = vcore
                },
                PowerOn = false,
            });

            Config.waitServerReady(result.Id);
            server = client.Servers.Show(result.Id);
            serverIds.Add(server.Id);

            //creating three servers, Private networks requires 3 servers to be created
            for (int i = 0; i < 2; i++)
            {
                Thread.Sleep(5000);
                var additional = Config.CreateTestServer("PN test .net" + i, false);
                Config.waitServerReady(additional.Id);
                serverIds.Add(additional.Id);
            }
        }

        [ClassCleanup]
        static public void ServerHardwareClean()
        {
            foreach (var id in serverIds)
            {
                if (id != null)
                {
                    Config.waitServerReady(id);
                    client.Servers.Delete(id, false);
                }
            }

            if (flavoredServer != null)
            {
                Config.waitServerReady(flavoredServer.Id);
                client.Servers.Delete(flavoredServer.Id, false);
            }
            if (clone != null)
            {
                Config.waitServerReady(clone.Id);
                client.Servers.Delete(clone.Id, false);
            }

            if (pn != null)
            {
                Config.waitPrivateNetworkReady(pn.Id);
                DeletePrivateNetwork();
            }
        }

        #region SERVER MAIN OPERTAIONS

        [TestMethod]
        public void UpdateServer()
        {
            var ranValue = new Random().Next(1, 1000);
            string udpatedName = "Updated" + ranValue;
            string updatedDesc = "Updated desc" + ranValue;
            var result = client.Servers.Update(new UpdateServerRequest()
            {
                Description = updatedDesc,
                Name = udpatedName
            }, server.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Name);
            Assert.IsNotNull(result.Hardware);
            //check if the values are udpated
            Assert.AreEqual(result.Name, udpatedName);
            Assert.AreEqual(result.Description, updatedDesc);
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
            var servers = client.Servers.Get(1, 3, null);

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
            string randomName = "ServerTestFlavor" + random.Next(9999);
            var availabeFixedImage = client.Servers.GetAvailableFixedServers()[1];

            var result = client.Servers.CreateServerFromFlavor(new POCO.Requests.Servers.CreateServerWithFlavorRequest()
            {
                ApplianceId = appliance.Id,
                Name = randomName,
                Description = "Example" + randomName,
                Hardware = new POCO.Requests.Servers.HardwareFlavorRequest()
                {
                    FixedInstanceSizeId = availabeFixedImage.Id
                },
                PowerOn = true
            });

            flavoredServer = client.Servers.Show(result.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Name);
            Assert.IsNotNull(result.Hardware);
            Config.waitServerReady(result.Id);

            UpdateServerStatus();
        }

        #endregion

        #region Secondary Operations

        [TestMethod]
        public void GetServerStatus()
        {
            var result = client.Servers.GetStatus(server.Id);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.State);
        }

        public void UpdateServerStatus()
        {
            var result = client.Servers.UpdateStatus(new UpdateStatusRequest()
            {
                Action = ServerAction.POWER_OFF,
                Method = ServerActionMethod.SOFTWARE
            }, flavoredServer.Id);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Status);
        }
        #endregion

        #region Private Networks

        [TestMethod]
        public void CreatePrivateNetwork()
        {
            string networkAddress = "192.168.1.0";
            string subnetMask = "255.255.255.0";
            var datacenters = client.DataCenters.Get();
            var dc = datacenters.FirstOrDefault();
            pn = client.PrivateNetworks.Create(new POCO.Requests.PrivateNetworks.CreatePrivateNetworkRequest
            {
                Name = "testPrivateNetwork .net",
                Description = "test description",
                NetworkAddress = networkAddress,
                SubnetMask = subnetMask,
                DatacenterId = dc.Id
            });
            Config.waitPrivateNetworkReady(pn.Id);
            var result = client.Servers.CreatePrivateNetwork(server.Id, pn.Id);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            Config.waitPrivateNetworkReady(pn.Id);
            var withPn = client.Servers.Show(result.Id);
            //check if the private network is added to the servers list
            Assert.IsTrue(withPn.PrivateNetworks.Any(pn => pn.Id == pn.Id));

            //test list private networks
            var pns = client.Servers.GetPrivateNetworks(server.Id);
            Assert.IsNotNull(pns);
            Assert.IsNotNull(pns.Count > 0);

            // test get private networks
            var spn = client.Servers.ShowPrivateNetworks(server.Id, withPn.PrivateNetworks[0].Id);
            Assert.IsNotNull(spn);
            Assert.IsNotNull(spn.Id);
        }

        public static void DeletePrivateNetwork()
        {
            var pnserver = client.Servers.Show(server.Id);
            Config.waitPrivateNetworkReady(pn.Id);
            Config.waitServerReady(pnserver.Id);
            if (pnserver.Status.State == ServerState.POWERED_ON)
            {
                //turn off server to remove PN
                var turnOff = client.Servers.UpdateStatus(new UpdateStatusRequest { Action = ServerAction.POWER_OFF, Method = ServerActionMethod.SOFTWARE }, pnserver.Id);
                Config.waitServerTurnedOff(pnserver.Id);
            }
            var result = client.Servers.DeletePrivateNetwork(pnserver.Id, pn.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }
        #endregion

        #region Snapshots

        public void GetSnapshots()
        {
            var result = client.Servers.GetSnapshots(serverIds.Last());
            Assert.IsNotNull(result);
        }

        public void UpdateSnapshots()
        {
            Config.waitServerReady(serverIds.Last());
            var snapshotServer = client.Servers.Show(serverIds.Last());
            var result = client.Servers.UpdateSnapshot(snapshotServer.Id, snapshotServer.Snapshot.Id);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void CreateSnapshots()
        {
            Config.waitServerReady(serverIds.Last());
            var result = client.Servers.CreateSnapshot(serverIds.Last());
            Config.waitServerReady(result.Id);
            result = client.Servers.Show(result.Id);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            Assert.IsNotNull(result.Snapshot);

            UpdateSnapshots();
            GetSnapshots();
            DeleteSnapshot();
        }

        public void DeleteSnapshot()
        {
            Config.waitServerReady(serverIds.Last());
            var snapshotServer = client.Servers.Show(serverIds.Last());
            var result = client.Servers.DeleteSnapshot(snapshotServer.Id, snapshotServer.Snapshot.Id);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check if the snapshot has deletion date
            Assert.IsNotNull(result.Snapshot.DeletionDate);
        }

        [TestMethod]
        public void CreateClone()
        {
            Config.waitServerReady(serverIds[1]);
            Random random = new Random();
            var result = client.Servers.CreateClone(serverIds[1], server.Name + "Clone" + random.Next(1000, 9999));
            clone = result;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        #endregion
    }
}
