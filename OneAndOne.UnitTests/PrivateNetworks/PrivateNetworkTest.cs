using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Response.Servers;
using OneAndOne.POCO;
using System.Threading;
using OneAndOne.POCO.Response.PrivateNetworks;
using System.Collections.Generic;

namespace OneAndOne.UnitTests.PrivateNetworks
{
    [TestClass]
    public class PrivateNetworkTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance(Config.Configuration);
        static PrivateNetworksResponse privateNetwork = null;
        static List<string> serverIds = new List<string>();

        [ClassInitialize]
        static public void TestInit(TestContext context)
        {
            string networkAddress = "192.168.1.0";
            string subnetMask = "255.255.255.0";
            var datacenters = client.DataCenters.Get();
            var dc = datacenters.FirstOrDefault();
            var privateNetworks = client.PrivateNetworks.Get();

            //creating three servers, Private networks requires 3 servers to be created
            for (int i = 0; i < 3; i++)
            {
                Thread.Sleep(5000);
                var server = Config.CreateTestServer("Private Network test .net111 " + i, false);
                Config.waitServerReady(server.Id);
                serverIds.Add(server.Id);
            }

            var result = client.PrivateNetworks.Create(new POCO.Requests.PrivateNetworks.CreatePrivateNetworkRequest()
            {
                Name = "testPrivateNetwork .net",
                Description = "test description",
                NetworkAddress = networkAddress,
                SubnetMask = subnetMask,
                DatacenterId = dc.Id
            });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            Config.waitPrivateNetworkReady(result.Id);
            //check if the network was created with correct values
            var privateNetworkResult = client.PrivateNetworks.Show(result.Id);
            privateNetwork = privateNetworkResult;
            Assert.IsNotNull(privateNetworkResult.Id);
            Assert.AreEqual(result.Name, privateNetworkResult.Name);
            Assert.AreEqual(result.NetworkAddress, privateNetworkResult.NetworkAddress);
            Assert.AreEqual(result.SubnetMask, privateNetworkResult.SubnetMask);

            //add a server to the private network
            var serverstoAdd = new System.Collections.Generic.List<string>();
            serverstoAdd.Add(serverIds[0]);
            var addedServer = client.PrivateNetworks.CreatePrivateNetworkServers(
            new POCO.Requests.PrivateNetworks.AttachPrivateNetworkServersRequest()
            {
                Servers = serverstoAdd
            }, privateNetwork.Id);

            Assert.IsNotNull(addedServer);
            Assert.IsNotNull(addedServer.Id);
            Config.waitPrivateNetworkReady(privateNetwork.Id);
            //check if the private network is assigned to the server
            var serverResult = client.Servers.Show(serverIds[0]);
            Assert.IsTrue(serverResult.PrivateNetworks.Any(pn => pn.Id == addedServer.Id));
        }

        [ClassCleanup]
        static public void TestClean()
        {
            if (privateNetwork != null)
            {
                Config.waitPrivateNetworkReady(privateNetwork.Id);
                Config.waitServerReady(serverIds[0]);
                var result = client.PrivateNetworks.DeletePrivateNetworksServer(privateNetwork.Id, serverIds[0]);

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
                Config.waitPrivateNetworkReady(privateNetwork.Id);
                //check if the private network is unassigned from the server
                var serverResult = client.Servers.Show(serverIds[0]);
                Assert.IsTrue(serverResult.PrivateNetworks == null);

                Config.waitPrivateNetworkReady(privateNetwork.Id);

                foreach (string id in serverIds)
                {
                    Config.waitServerReady(id);
                    client.Servers.Delete(id, false);
                }

                Config.waitPrivateNetworkReady(privateNetwork.Id);
                //remove the private network
                var removed = client.PrivateNetworks.Delete(privateNetwork.Id);

                Assert.IsNotNull(removed);
                Assert.IsNotNull(removed.Id);
                //check if the network was removed
                var privateNetworkResult = client.PrivateNetworks.Get();
                var exists = privateNetworkResult.FirstOrDefault(pn => pn.Id == privateNetwork.Id);
                if (exists != null && exists.State == "REMOVING")
                    return;
                Assert.IsFalse(privateNetworkResult.Any(pn => pn.Id == privateNetwork.Id));
            }

        }


        [TestMethod]
        public void GetPrivateNetworks()
        {
            var result = client.PrivateNetworks.Get();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShowPrivateNetwork()
        {
            var result = client.PrivateNetworks.Show(privateNetwork.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void UpdatePrivateNetwork()
        {
            Random random = new Random();
            string networkAddress = "192.168.1.0";
            string subnetMask = "255.255.255.0";
            var result = client.PrivateNetworks.Update(new POCO.Requests.PrivateNetworks.UpdatePrivateNetworkRequest()
            {
                Name = "updated" + privateNetwork.Name,
                NetworkAddress = networkAddress,
                SubnetMask = subnetMask,
            }, privateNetwork.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            Config.waitPrivateNetworkReady(privateNetwork.Id);
            //check if the network was updated with correct values
            var privateNetworkResult = client.PrivateNetworks.Show(result.Id);
            Assert.IsNotNull(privateNetworkResult.Id);
            Assert.AreEqual(result.Name, privateNetworkResult.Name);
            Assert.AreEqual(result.NetworkAddress, privateNetworkResult.NetworkAddress);
            Assert.AreEqual(result.SubnetMask, privateNetworkResult.SubnetMask);
        }

        #region private networks servers
        [TestMethod]
        public void GetPrivateNetworksServers()
        {
            var result = client.PrivateNetworks.GetPrivateNetworkServers(privateNetwork.Id);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShowPrivateNetworkServer()
        {
            var updatedPN = client.PrivateNetworks.Show(privateNetwork.Id);
            var result = client.PrivateNetworks.ShowPrivateNetworkServer(privateNetwork.Id, updatedPN.Servers[0].Id);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        #endregion

    }
}
