using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Respones.SharedStorages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.UnitTests.PrivateNetworks
{
    [TestClass]
    public class PrivateNetworkServerstest
    {
        static OneAndOneClient client = new OneAndOneClient();
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
            var privateNetwork = privateNetworks[random.Next(privateNetworks.Count - 1)];
            var servers = client.Servers.Get();
            var randomServer = servers[random.Next(0, servers.Count - 1)];
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
                try
                {
                    var result = client.PrivateNetworks.CreatePrivateNetworkServers(
                    new POCO.Requests.PrivateNetworks.AttachPrivateNetworkServersRequest()
                    {
                        Servers = serverstoAdd

                    }, privateNetwork.Id);

                    Assert.IsNotNull(result);
                    Assert.IsNotNull(result.Id);
                }
                catch (Exception ex)
                {
                    if (!ex.Message.Contains("Generic error"))
                        throw;
                }

            }
        }

        [TestMethod]
        public void DeleteSharedStoragesServers()
        {
            Random random = new Random();
            var privateNetworks = client.PrivateNetworks.Get().Where(pn => pn.Name.Contains("testPrivateNetwork")).ToList();
            var privateNetwork = privateNetworks[random.Next(privateNetworks.Count - 1)];
            foreach (var item in privateNetworks)
            {
                if (item.Servers != null && item.Servers.Count > 0)
                {
                    privateNetwork = item;
                    break;
                }
            }
            if (privateNetwork != null && privateNetwork.Servers != null && privateNetwork.Servers.Count > 0)
            {
                var result = client.PrivateNetworks.DeletePrivateNetworksServer(privateNetwork.Id, privateNetwork.Servers[0].Id);

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
            }
        }
    }
}
