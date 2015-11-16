using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;

namespace OneAndOne.UnitTests.PrivateNetworks
{
    [TestClass]
    public class PrivateNetworkTest
    {
        static OneAndOneClient client = new OneAndOneClient();
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
            Random random = new Random();
            var result = client.PrivateNetworks.Create(new POCO.Requests.PrivateNetworks.CreatePrivateNetworkRequest()
                {
                    Name = random.Next(1000, 9999) + "testPrivateNetwork",
                    Description = "test description",
                });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void UpdatePrivateNetwork()
        {
            Random random = new Random();
            var privateNetworks = client.PrivateNetworks.Get().Where(pn => pn.Name.Contains("testPrivateNetwork")).ToList();
            var privateNetwork = privateNetworks[random.Next(privateNetworks.Count - 1)];
            var result = client.PrivateNetworks.Update(new POCO.Requests.PrivateNetworks.UpdatePrivateNetworkRequest()
                {
                    Name = "updated" + privateNetwork.Name,
                }, privateNetwork.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void DeletePrivateNetwork()
        {
            Random random = new Random();
            var privateNetworks = client.PrivateNetworks.Get().Where(pn => pn.Name.Contains("testPrivateNetwork")).ToList();
            var privateNetwork = privateNetworks[random.Next(privateNetworks.Count - 1)];
            var result = client.PrivateNetworks.Delete(privateNetwork.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void DeleteAllTestPrivateNetworks()
        {
            Random random = new Random();
            var privateNetworks = client.PrivateNetworks.Get().Where(pn => pn.Name.Contains("testPrivateNetwork")).ToList();
            foreach (var item in privateNetworks)
            {
                var result = client.PrivateNetworks.Delete(item.Id);
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
            }
        }
    }
}
