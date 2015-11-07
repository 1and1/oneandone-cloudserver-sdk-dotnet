using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;

namespace OneAndOne.UnitTests
{
    [TestClass]
    public class ServerHardwareTest
    {

        static OneAndOneClient client = new OneAndOneClient();
        [TestMethod]
        public void GetServerHardWare()
        {
            var servers = client.Servers.Get().FirstOrDefault();
            var result = client.ServersHardware.Show(servers.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.CoresPerProcessor);
        }
    }
}
