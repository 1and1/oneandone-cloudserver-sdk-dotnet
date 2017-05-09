using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;

namespace OneAndOne.UnitTests.Common
{
    [TestClass]
    public class CommonTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance(Config.Configuration);

        [TestMethod]
        public void GetPricing()
        {
            var result = client.Common.GetPricing();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetDataCenters()
        {
            var result = client.DataCenters.Get();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShowDataCenter()
        {
            var dcList = client.DataCenters.Get();

            var result = client.DataCenters.Show(dcList[0].Id);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void PingApi()
        {
            var result = client.Common.Ping();
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void PingApiAuthentication()
        {
            var result = client.Common.PingAuthentication();
            Assert.IsTrue(result);
        }
    }
}
