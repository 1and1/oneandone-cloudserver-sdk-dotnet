using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Requests.Servers;
using OneAndOne.POCO.Response.PublicIPs;

namespace OneAndOne.UnitTests.PublicIPs
{
    [TestClass]
    public class PublicIPsTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance(Config.Configuration);
        static PublicIPsResponse ip = null;


        [ClassInitialize]
        static public void TestInit(TestContext context)
        {
            var datacenters = client.DataCenters.Get();
            var dc = datacenters.FirstOrDefault();
            Random random = new Random();
            var randomValue = random.Next(10, 99) + "netTest.net";
            var result = client.PublicIPs.Create(new POCO.Requests.PublicIPs.CreatePublicIPRequest()
            {
                ReverseDns = randomValue,
                Type = IPType.IPV4
            });

            ip = result;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check if the public ip was created
            var ipResult = client.PublicIPs.Show(result.Id);
            Assert.IsNotNull(ipResult.Id);
            Assert.AreEqual(ipResult.ReverseDns, randomValue);
        }

        [ClassCleanup]
        static public void TestClean()
        {
            if (ip != null)
            {
                DeletePublicIP();
            }
        }

        [TestMethod]
        public void GetPublicIPs()
        {
            var result = client.PublicIPs.Get();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void ShowPublicIP()
        {
            var result = client.PublicIPs.Show(ip.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void UpdatePublicIP()
        {
            Random random = new Random();
            var randomValue = random.Next(10, 99) + "updateTest.net";
            var result = client.PublicIPs.Update(randomValue, ip.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check if the public ip updated
            var ipResult = client.PublicIPs.Show(result.Id);
            Assert.IsNotNull(ipResult.Id);
            Assert.AreEqual(ipResult.ReverseDns, randomValue);
        }

        static public void DeletePublicIP()
        {
            var result = client.PublicIPs.Delete(ip.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }
    }
}
