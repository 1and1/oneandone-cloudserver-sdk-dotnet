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

        [TestMethod]
        public void UpdateServerHardWare()
        {
            var random = new Random();
            var servers = client.Servers.Get();
            var server = servers[random.Next(servers.Count)];
            int CoresPerProcessor = 4;
            int Ram = 4;
            int Vcore = 4;
            if (server.Hardware.CoresPerProcessor > CoresPerProcessor && server.Hardware.Ram > Ram && server.Hardware.Vcore > Vcore)
            {

                var result = client.ServersHardware.Update(new POCO.Requests.Servers.UpdateHardwareRequest()
                    {
                        CoresPerProcessor = CoresPerProcessor,
                        Ram = 4,
                        Vcore = 8
                    }, server.Id);

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Hardware.CoresPerProcessor);
                Assert.AreEqual(result.Hardware.CoresPerProcessor, 3);
                Assert.AreEqual(result.Hardware.Ram, 4);
                Assert.AreEqual(result.Hardware.Vcore, 5);
            }
        }

        [TestMethod]
        public void GetServerHardDrives()
        {
            var server = client.Servers.Get().FirstOrDefault();
            var result = client.ServerHardwareHdd.Show(server.Id);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void AddServerHardDrives()
        {
            var server = client.Servers.Get().FirstOrDefault();
            var result = client.ServerHardwareHdd.Update(new POCO.Requests.Servers.AddHddRequest()
                {
                    Hdds = new System.Collections.Generic.List<POCO.Requests.Servers.HddRequest>()
                    {
                        { new POCO.Requests.Servers.HddRequest()
                        {Size=20,IsMain=false}},
                        {new POCO.Requests.Servers.HddRequest()
                        {Size=30,IsMain=false}
                    }}
                }, server.Id);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Hardware.Hdds.Count > 0);

        }
    }
}
