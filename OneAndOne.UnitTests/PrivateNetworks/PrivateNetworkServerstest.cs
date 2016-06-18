using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Response.Servers;
using OneAndOne.POCO.Response.SharedStorages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OneAndOne.UnitTests.PrivateNetworks
{
    [TestClass]
    public class PrivateNetworkServerstest
    {
        static OneAndOneClient client = OneAndOneClient.Instance();
        [TestMethod]
        public void GetPrivateNetworksServers()
        {
            Random random = new Random();
            var privateNetworks = client.PrivateNetworks.Get();
            var privateNetwork = privateNetworks[random.Next(privateNetworks.Count - 1)];
            var result = client.PrivateNetworks.GetPrivateNetworkServers(privateNetwork.Id);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShowPrivateNetworkServer()
        {
            Random random = new Random();
            var privateNetworks = client.PrivateNetworks.Get();
            var privateNetwork = privateNetworks[random.Next(privateNetworks.Count - 1)];
            foreach (var item in privateNetworks)
            {
                if (item.Servers != null && item.Servers.Count > 0)
                {
                    privateNetwork = item;
                    break;
                }
            }
            if (privateNetwork.Servers != null && privateNetwork.Servers.Count > 0)
            {
                var result = client.PrivateNetworks.ShowPrivateNetworkServer(privateNetwork.Id, privateNetwork.Servers[0].Id);
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
            }
        }

        [TestMethod]
        public void AssignPrivateNetworkServer()
        {
            Random random = new Random();
            var privateNetworks = client.PrivateNetworks.Get();
            var privateNetwork = privateNetworks[0];
            var servers = client.Servers.Get();
            var randomServer = servers[0];
            foreach (var item in servers)
            {
                Thread.Sleep(1000);
                var server = client.Servers.Show(item.Id);
                if (server.Snapshot == null)
                {
                    randomServer = server;
                    break;
                }
            }
            foreach (var item in privateNetworks)
            {
                if (!item.Servers.Any(itm => itm.Id == randomServer.Id))
                {
                    privateNetwork = item;
                    break;
                }
            }
            if (servers.Count > 0)
            {
                var serverstoAdd = new System.Collections.Generic.List<string>();
                serverstoAdd.Add(randomServer.Id);
                if (privateNetwork.Servers.Any(itm => itm.Id == randomServer.Id))
                {
                    return;
                }
                var result = client.PrivateNetworks.CreatePrivateNetworkServers(
                new POCO.Requests.PrivateNetworks.AttachPrivateNetworkServersRequest()
                {
                    Servers = serverstoAdd
                }, privateNetwork.Id);

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
                //check if the private network is assigned to the server
                var serverResult = client.Servers.Show(randomServer.Id);
                Assert.IsTrue(serverResult.PrivateNetworks.Any(pn => pn.Id == result.Id));
            }
        }

        [TestMethod]
        public void DeletePrivateNetworksServer()
        {
            Random random = new Random();
            var privateNetworks = client.PrivateNetworks.Get().Where(pn => pn.Name.Contains("testPrivateNetwork")).ToList();
            var privateNetwork = privateNetworks[random.Next(privateNetworks.Count - 1)];
            int indexOfserver = 0;
            foreach (var item in privateNetworks)
            {
                if (item.Servers != null && item.Servers.Count > 0)
                {
                    foreach (var server in item.Servers)
                    {
                        Thread.Sleep(1000);
                        var fromApi = client.Servers.Show(server.Id);
                        if (fromApi.Status.State == ServerState.POWERED_OFF || (!fromApi.Image.Name.Contains("ub") && !fromApi.Image.Name.Contains("centos")))
                        {
                            privateNetwork = item;
                            indexOfserver = item.Servers.IndexOf(server);
                            break;
                        }
                    }
                }
                return;
            }
            if (privateNetwork != null && privateNetwork.Servers != null && privateNetwork.Servers.Count > 0)
            {
                var result = client.PrivateNetworks.DeletePrivateNetworksServer(privateNetwork.Id, privateNetwork.Servers[indexOfserver].Id);

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
                //check if the private network is unassigned from the server
                var serverResult = client.Servers.Show(privateNetwork.Servers[0].Id);
                Assert.IsFalse(serverResult.PrivateNetworks.Any(pn => pn.Id == result.Id));
            }
        }
    }
}
