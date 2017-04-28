using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using System.Linq;
using OneAndOne.POCO.Response.Vpn;
using System.Threading;

namespace OneAndOne.UnitTests.Vpn
{
    [TestClass]
    public class VpnTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance(Config.Configuration);
        static VpnResponse vpn = null;

        [ClassInitialize]
        static public void TestInit(TestContext context)
        {
            var datacenters = client.DataCenters.Get();
            var dc = datacenters.FirstOrDefault();
            Random random = new Random();
            var result = client.Vpn.Create(new POCO.Requests.Vpn.CreateVpnRequest
            {
                Name = "vpn test",
                Description = "desc",
                Datacenterid = dc.Id
            });

            vpn = result;
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [ClassCleanup]
        static public void TestClean()
        {
            if (vpn != null)
            {
                DeleteVpn();
            }
        }

        [TestMethod]
        public void GetVpns()
        {
            var result = client.Vpn.Get();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void ShowVpn()
        {
            var result = client.Vpn.Show(vpn.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void Configuration()
        {
            Thread.Sleep(5000);
            var result = client.Vpn.ShowConfiguration(vpn.Id);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UpdateVpn()
        {
            Random random = new Random();
            var randomValue = random.Next(10, 99) + "updateTest.net";
            var result = client.Vpn.Update(new POCO.Requests.Vpn.UpdateVpnRequest
            {
                Name = "updated name",
                Description = "desc update"
            }, vpn.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check if the vpn updated
            var updated = client.Vpn.Show(result.Id);
            Assert.IsNotNull(updated.Id);
            Assert.AreEqual(updated.Name, "updated name");
        }

        static public void DeleteVpn()
        {
            var result = client.Vpn.Delete(vpn.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }
    }
}
