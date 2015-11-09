using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Requests.Servers;
using OneAndOne.POCO.Respones.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.UnitTests
{
    [TestClass]
    public class ServerIpsTest
    {
        static OneAndOneClient client = new OneAndOneClient();
        Random random = new Random();

        [TestMethod]
        public void GetServerIPList()
        {
            var servers = client.Servers.Get();
            var server = servers[random.Next(servers.Count - 1)];
            if (server.Status.State == ServerState.DEPLOYING || server.Status.State == ServerState.REMOVING)
            {
                return;
            }
            var result = client.ServerIps.Get(server.Id);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void AddServerIP()
        {
            var servers = client.Servers.Get();
            var server = servers[random.Next(servers.Count - 1)];
            if (server.Status.State == ServerState.DEPLOYING || server.Status.State == ServerState.REMOVING)
            {
                return;
            }
            var result = client.ServerIps.Create(new POCO.Requests.Servers.CreateServerIPRequest()
                {
                    Type = IPType.IPV4
                }, server.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }
    }
}
