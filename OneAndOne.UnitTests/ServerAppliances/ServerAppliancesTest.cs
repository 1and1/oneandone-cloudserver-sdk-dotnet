using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;

namespace OneAndOne.UnitTests.ServerAppliances
{
    [TestClass]
    public class ServerAppliancesTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance(Config.Configuration);
        [TestMethod]
        public void GetServerAppliances()
        {
            var result = client.ServerAppliances.Get();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetServerAppliance()
        {
            var appliances = client.ServerAppliances.Get();

            var result = client.ServerAppliances.Show(appliances[0].Id);

            Assert.IsNotNull(result);
        }
    }
}
