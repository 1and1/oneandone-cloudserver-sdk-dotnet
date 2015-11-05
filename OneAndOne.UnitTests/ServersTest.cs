using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;

namespace OneAndOne.UnitTests
{
    [TestClass]
    public class ServersTest
    {
        static OneAndOneClient client = new OneAndOneClient();
        [TestMethod]
        public void GetServers()
        {
            var servers = client.Servers.GetServers();

            Assert.IsNotNull(servers);
            Assert.IsTrue(servers.Count > 0);
        }
        [TestMethod]
        public void GetServersWithPaging()
        {
            var servers = client.Servers.GetServers(1, 3);

            Assert.IsNotNull(servers);
            Assert.IsTrue(servers.Count > 0);
        }


    }
}
