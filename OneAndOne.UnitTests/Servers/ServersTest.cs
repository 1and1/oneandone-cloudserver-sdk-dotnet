using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using System.Collections.Generic;
using OneAndOne.POCO.Requests.Servers;
using OneAndOne.POCO.Respones.Servers;
using System.Threading;
using OneAndOne.POCO;
using OneAndOne.POCO.Respones.ServerAppliances;

namespace OneAndOne.UnitTests
{
    [TestClass]
    public class ServersTest
    {
        //Add the API Token and the API URL here to test
        static OneAndOneClient client = OneAndOneClient.Instance();
        string randomName;

        public ServersTest()
        {
            Random random = new Random();
            randomName = "ServerTest" + random.Next(1000, 9999);
        }

        #region SERVER MAIN OPERTAIONS

        [TestMethod]
        public void CreateServer()
        {
            int vcore = 4;
            int CoresPerProcessor = 2;
            var servers = client.Servers.Get();
            if (servers.Any(ser => ser.Name == randomName))
            {
                return;
            }
            var appliances = client.ServerAppliances.Get().Where(app => app.OsFamily == OSFamliyType.Windows && app.AutomaticInstallation == true);
            ServerAppliancesResponse appliance = null;
            if (appliances == null || appliances.Count() == 0)
            {
                appliance = client.ServerAppliances.Get().FirstOrDefault();
            }
            else
            {
                appliance = appliances.FirstOrDefault();
            }
            var publicIP = client.PublicIPs.Get().FirstOrDefault(ip => ip.State == "ACTIVE" && ip.AssignedTo == null);
            var result = client.Servers.Create(new POCO.Requests.Servers.CreateServerRequest()
            {
                ApplianceId = appliance != null ? appliance.Id : null,
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
                                Size=appliance.MinHddSize,
                            }}
                        },
                    Ram = 4,
                    Vcore = vcore
                },
                PowerOn = true,
                Password = "Test123!",
                IpId = publicIP != null ? publicIP.Id : null
            });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Name);
            Assert.IsNotNull(result.Hardware);
            Assert.IsNotNull(result.Status.Percent);
        }

        [TestMethod]
        public void UpdateServer()
        {
            Random random = new Random();
            var servers = client.Servers.Get();
            var server = client.Servers.Show(servers[random.Next(0, servers.Count - 1)].Id);
            foreach (var item in client.Servers.Get())
            {
                if (item.Name.Contains("ServerTest") && (
                   item.Status.State != ServerState.POWERING_ON && item.Status.State != ServerState.DEPLOYING
                   && item.Status.State != ServerState.CONFIGURING && item.Status.State != ServerState.REBOOTING))
                {
                    server = item;
                    break;
                }
            }
            var ranValue = new Random().Next(1, 1000);
            string udpatedName = "Updated" + ranValue;
            string updatedDesc = "Updated desc" + ranValue;
            if (server != null)
            {
                if (!server.Name.Contains("Updated"))
                {
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
            //TODO: Add functional test to check the availablity of server
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
                //check server state is removing
                Assert.AreEqual(result.Status.State, ServerState.REMOVING);
            }
        }

        [TestMethod]
        public void DeleteAllTestServers()
        {
            var servers = client.Servers.Get().Where(ser => ser.Name.Contains("ServerTest") || ser.Name.Contains("Updated"));
            foreach (var item in servers)
            {
                if (item.Status.State == ServerState.POWERED_ON || item.Status.State == ServerState.POWERED_OFF)
                {
                    var result = client.Servers.Delete(item.Id, false);
                    Assert.IsNotNull(result);
                    Assert.IsNotNull(result.Name);
                    Assert.IsNotNull(result.Hardware);
                }
            }
        }

        #endregion

        #region Secondary Operations

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
            ServerAction newState = ServerAction.REBOOT;
            var server = servers[random.Next(servers.Count - 1)];
            foreach (var item in servers)
            {
                if (item.Status.State == ServerState.POWERED_ON || item.Status.State == ServerState.POWERED_OFF)
                {
                    server = item;
                    break;
                }
            }
            if (server.Status.State == ServerState.POWERED_OFF)
            {
                newState = ServerAction.POWER_ON;

            }
            var result = client.Servers.UpdateStatus(new UpdateStatusRequest()
            {
                Action = newState,
                Method = ServerActionMethod.SOFTWARE
            }, server.Id);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Status);
            //check server state is either rebooting or powering on
            Assert.AreEqual(result.Status.State, newState == ServerAction.REBOOT ? ServerState.REBOOTING : ServerState.POWERING_ON);

        }
        #endregion

        #region Private Networks
        [TestMethod]
        public void GetPrivateNetworks()
        {
            Random random = new Random();
            var servers = client.Servers.Get();
            var server = servers[random.Next(servers.Count - 1)];

            var result = client.Servers.GetPrivateNetworks(server.Id);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Count > 0);
        }

        [TestMethod]
        public void ShowPrivateNetworks()
        {
            var servers = client.Servers.Get();
            foreach (var item in servers)
            {
                Thread.Sleep(1000);
                var server = client.Servers.Show(item.Id);
                if (server.PrivateNetworks != null && server.PrivateNetworks.Count > 0)
                {

                    var result = client.Servers.ShowPrivateNetworks(server.Id, server.PrivateNetworks[0].Id);
                    Assert.IsNotNull(result);
                    Assert.IsNotNull(result.Id);
                    break;
                }
            }
        }

        [TestMethod]
        public void CreatePrivateNetwork()
        {
            Random random = new Random();
            var servers = client.Servers.Get();
            foreach (var server in servers.Where(p => p.Name.Contains("ServerTest")))
            {
                var privateNetworks = client.PrivateNetworks.Get();
                if (privateNetworks == null || privateNetworks.Count == 0)
                {
                    return;
                }

                var privateNetwork = privateNetworks[0];
                var curServer = client.Servers.Show(server.Id);
                if (curServer.Status.State == ServerState.POWERING_ON || curServer.Snapshot != null)
                    continue;
                if (server.PrivateNetworks == null || !server.PrivateNetworks.Any(pn => pn.Id == privateNetwork.Id))
                {
                    privateNetwork = privateNetworks[0];
                }
                var result = client.Servers.CreatePrivateNetwork(curServer.Id, privateNetwork.Id);
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
                //check if the private network is added to the servers list
                Assert.IsTrue(result.PrivateNetworks.Any(pn => pn.Id == privateNetwork.Id));
                break;
            }
        }

        [TestMethod]
        public void DeletePrivateNetwork()
        {
            var servers = client.Servers.Get();
            foreach (var item in servers)
            {
                Thread.Sleep(1000);
                var server = client.Servers.Show(item.Id);
                //this check is here becuase i get the following error when trying to delete netwotk on ubuntu os
                //Network interface cannot be hot removed in Ubuntu virtual machines
                if (server.Image.Name.Contains("ub") || server.Snapshot != null || server.Status.Percent > 0)
                    continue;
                if (server.PrivateNetworks != null && server.PrivateNetworks.Count > 0)
                {
                    var currentPrivateNetwork = client.Servers.ShowPrivateNetworks(server.Id, server.PrivateNetworks[0].Id);
                    while (currentPrivateNetwork.State == "CONFIGURING")
                    {
                        Thread.Sleep(1500);
                        currentPrivateNetwork = client.Servers.ShowPrivateNetworks(server.Id, server.PrivateNetworks[0].Id);
                    }
                    var result = client.Servers.DeletePrivateNetwork(server.Id, server.PrivateNetworks[0].Id);
                    Assert.IsNotNull(result);
                    Assert.IsNotNull(result.Id);
                    //give the server time to update
                    var resultserver = client.Servers.Show(result.Id);
                    while (resultserver.PrivateNetworks != null && resultserver.PrivateNetworks.Any(pn => pn.Id == server.PrivateNetworks[0].Id))
                    {
                        Thread.Sleep(5000);
                        resultserver = client.Servers.Show(result.Id);
                    }
                    //check if the private network is gone from the servers list
                    Assert.IsTrue(resultserver.PrivateNetworks == null || !resultserver.PrivateNetworks.Any(pn => pn.Id == server.PrivateNetworks[0].Id));
                    break;

                }
            }
        }
        #endregion

        #region Snapshots

        [TestMethod]
        public void GetSnapshots()
        {
            Random random = new Random();
            var servers = client.Servers.Get();
            var server = servers[random.Next(servers.Count - 1)];

            var result = client.Servers.GetSnapshots(server.Id);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UpdateSnapshots()
        {
            var servers = client.Servers.Get();
            foreach (var item in servers)
            {
                Thread.Sleep(1000);
                var server = client.Servers.Show(item.Id);
                if (server.Snapshot != null)
                {
                    if (server.Status.Percent == 0 && server.Status.State == ServerState.POWERED_OFF)
                    {
                        var result = client.Servers.UpdateSnapshot(server.Id, server.Snapshot.Id);
                        Assert.IsNotNull(result);
                        Assert.IsNotNull(result.Id);
                        break;
                    }
                }
            }
        }

        [TestMethod]
        public void CreateSnapshots()
        {
            Random random = new Random();
            var servers = client.Servers.Get().Where(ser => ser.Name.Contains("ServerTest") || ser.Name.Contains("Updated")); ;
            foreach (var item in servers)
            {
                Thread.Sleep(1000);
                var server = client.Servers.Show(item.Id);
                if (server.Snapshot == null)
                {
                    var result = client.Servers.CreateSnapshot(server.Id);
                    Assert.IsNotNull(result);
                    Assert.IsNotNull(result.Id);
                    Assert.IsNotNull(result.Snapshot);
                    //check if the snapshot is newly added
                    Assert.IsTrue(result.Snapshot.CreationDate <= DateTime.Now && result.Snapshot.CreationDate > DateTime.Now.AddHours(-2));
                    break;
                }
            }
        }

        [TestMethod]
        public void DeleteSnapshot()
        {
            var servers = client.Servers.Get().Where(ser => ser.Name.Contains("ServerTest") || ser.Name.Contains("Updated"));
            foreach (var item in servers)
            {
                Thread.Sleep(1000);
                var server = client.Servers.Show(item.Id);
                if (server.Snapshot != null)
                {
                    var result = client.Servers.DeleteSnapshot(server.Id, server.Snapshot.Id);
                    Assert.IsNotNull(result);
                    Assert.IsNotNull(result.Id);
                    //check if the snapshot has deletion date
                    Assert.IsNotNull(result.Snapshot.DeletionDate);
                    break;
                }
            }
        }

        [TestMethod]
        public void CreateClone()
        {
            Random random = new Random();
            var servers = client.Servers.Get();
            foreach (var item in servers)
            {
                Thread.Sleep(1000);
                var server = client.Servers.Show(item.Id);
                if (server.Snapshot == null && server.Status.Percent == 0)
                {
                    var result = client.Servers.CreateClone(server.Id, server.Name + "Clone" + random.Next(1000, 9999));
                    Assert.IsNotNull(result);
                    Assert.IsNotNull(result.Id);
                    break;
                }
            }
        }

        #endregion
    }
}
