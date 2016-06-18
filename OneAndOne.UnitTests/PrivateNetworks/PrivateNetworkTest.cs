using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Response.Servers;
using OneAndOne.POCO;
using System.Threading;

namespace OneAndOne.UnitTests.PrivateNetworks
{
    [TestClass]
    public class PrivateNetworkTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance();
        [TestMethod]
        public void GetPrivateNetworks()
        {
            var result = client.PrivateNetworks.Get();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShowPrivateNetwork()
        {
            Random random = new Random();
            var privateNetworks = client.PrivateNetworks.Get();
            var privateNetwork = privateNetworks[random.Next(privateNetworks.Count - 1)];

            var result = client.PrivateNetworks.Show(privateNetwork.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void CreatePrivateNetwork()
        {
            var privateNetworks = client.PrivateNetworks.Get();
            //we are not allowed to create more than two by the account            
            if (privateNetworks.Count == 2)
                return;
            Random random = new Random();
            string networkAddress = "192.168.1.0";
            string subnetMask = "255.255.255.0";
            var result = client.PrivateNetworks.Create(new POCO.Requests.PrivateNetworks.CreatePrivateNetworkRequest()
                {
                    Name = random.Next(1000, 9999) + "testPrivateNetwork",
                    Description = "test description",
                    NetworkAddress = networkAddress,
                    SubnetMask = subnetMask
                });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check if the network was created with correct values
            var privateNetworkResult = client.PrivateNetworks.Show(result.Id);
            Assert.IsNotNull(privateNetworkResult.Id);
            Assert.AreEqual(result.Name, privateNetworkResult.Name);
            Assert.AreEqual(result.NetworkAddress, privateNetworkResult.NetworkAddress);
            Assert.AreEqual(result.SubnetMask, privateNetworkResult.SubnetMask);

        }

        [TestMethod]
        public void UpdatePrivateNetwork()
        {
            Random random = new Random();
            string networkAddress = "192.168.1.0";
            string subnetMask = "255.255.255.0";
            var privateNetworks = client.PrivateNetworks.Get().Where(pn => pn.Name.Contains("testPrivateNetwork")).ToList();
            var privateNetwork = privateNetworks[random.Next(privateNetworks.Count - 1)];
            var result = client.PrivateNetworks.Update(new POCO.Requests.PrivateNetworks.UpdatePrivateNetworkRequest()
                {
                    Name = "updated" + privateNetwork.Name,
                    NetworkAddress = networkAddress,
                    SubnetMask = subnetMask,
                }, privateNetwork.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check if the network was updated with correct values
            var privateNetworkResult = client.PrivateNetworks.Show(result.Id);
            Assert.IsNotNull(privateNetworkResult.Id);
            Assert.AreEqual(result.Name, privateNetworkResult.Name);
            Assert.AreEqual(result.NetworkAddress, privateNetworkResult.NetworkAddress);
            Assert.AreEqual(result.SubnetMask, privateNetworkResult.SubnetMask);
        }

        [TestMethod]
        public void DeletePrivateNetwork()
        {
            Random random = new Random();
            var privateNetworks = client.PrivateNetworks.Get().Where(pn => pn.Name.Contains("testPrivateNetwork")).ToList();
            var privateNetwork = privateNetworks[0];
            foreach (var item in privateNetworks)
            {
                if (item.State != "ACTIVE")
                { return; }
                privateNetwork = item;
                foreach (var server in item.Servers)
                {
                    var fromApi = client.Servers.Show(server.Id);
                    if (fromApi.Status.State != ServerState.POWERED_OFF)
                    {
                        client.Servers.UpdateStatus(new POCO.Requests.Servers.UpdateStatusRequest()
                        {
                            Action = ServerAction.POWER_OFF,
                            Method = ServerActionMethod.SOFTWARE
                        }, fromApi.Id);

                        var status = client.Servers.GetStatus(fromApi.Id);
                        while (status.State != ServerState.POWERED_OFF || status.Percent < 50)
                        {
                            Thread.Sleep(1000);
                            status = client.Servers.GetStatus(fromApi.Id);
                        }
                        break;
                    }
                    else
                    {
                        break;

                    }
                }
            }
            var result = client.PrivateNetworks.Delete(privateNetwork.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check if the network was removed
            var privateNetworkResult = client.PrivateNetworks.Get();
            var exists = privateNetworkResult.FirstOrDefault(pn => pn.Id == privateNetwork.Id);
            if (exists != null && exists.State == "REMOVING")
                return;
            Assert.IsFalse(privateNetworkResult.Any(pn => pn.Id == privateNetwork.Id));
        }

        [TestMethod]
        public void DeleteAllTestPrivateNetworks()
        {
            Random random = new Random();
            var privateNetworks = client.PrivateNetworks.Get().Where(pn => pn.Name.Contains("testPrivateNetwork")).ToList();
            foreach (var item in privateNetworks)
            {
                foreach (var server in item.Servers)
                {
                    var fromApi = client.Servers.Show(server.Id);
                    if (fromApi.Status.State != ServerState.POWERED_OFF)
                    {
                        client.Servers.UpdateStatus(new POCO.Requests.Servers.UpdateStatusRequest()
                            {
                                Action = ServerAction.POWER_OFF,
                                Method = ServerActionMethod.SOFTWARE
                            }, fromApi.Id);

                        var status = client.Servers.GetStatus(fromApi.Id);
                        while (status == null || status.State != ServerState.POWERED_OFF)
                        {
                            Thread.Sleep(1000);
                            status = client.Servers.GetStatus(fromApi.Id);
                        }
                    }
                }
                var result = client.PrivateNetworks.Delete(item.Id);
            }
        }
    }
}
