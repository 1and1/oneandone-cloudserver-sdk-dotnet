using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using System.Threading;
using OneAndOne.POCO.Respones.Servers;

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
            var server = servers[random.Next(servers.Count - 1)];
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
            var result = client.ServerHdds.Get(server.Id);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void ShowHardDrives()
        {
            var random = new Random();
            var servers = client.Servers.Get();
            var server = servers[random.Next(servers.Count - 1)];

            var result = client.ServerHdds.Show(server.Id, server.Hardware.Hdds[random.Next(server.Hardware.Hdds.Count - 1)].Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void AddServerHardDrives()
        {
            var random = new Random();
            var servers = client.Servers.Get();
            var server = servers[random.Next(servers.Count - 1)];
            if (server.Hardware.Hdds.Count < 8)
            {
                var result = client.ServerHdds.Create(new POCO.Requests.Servers.AddHddRequest()
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

        [TestMethod]
        public void UpdateHardDrives()
        {
            var random = new Random();
            var servers = client.Servers.Get();
            var server = servers[random.Next(servers.Count - 1)];
            var randomHdd = server.Hardware.Hdds[random.Next(server.Hardware.Hdds.Count - 1)];
            if (server.Status.State == ServerState.REMOVING)
            {
                return;
            }
            int size = 20;
            if (randomHdd.Size < 100)
                size = 120;
            else
            {
                size = randomHdd.Size + 20;
            }
            if (randomHdd.Size == 2000 || randomHdd.Size > size)
            {
                return;
            }
            var result = client.ServerHdds.Update(new POCO.Requests.Servers.UpdateHddRequest()
                {
                    Size = size
                }, server.Id, randomHdd.Id);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteHardDrive()
        {
            var random = new Random();
            var servers = client.Servers.Get();
            var server = servers[random.Next(servers.Count - 1)];
            if (server.Status.State == ServerState.REMOVING)
            {
                return;
            }
            int serverTries = 0;
            int hddTries = 0;
            var randomHdd = server.Hardware.Hdds[random.Next(server.Hardware.Hdds.Count - 1)];

            while (server.Hardware.Hdds.Count == 1 && serverTries < 15)
            {
                server = servers[random.Next(servers.Count - 1)];
                ++serverTries;
            }
            while (randomHdd.IsMain && hddTries < 15)
            {
                randomHdd = server.Hardware.Hdds[random.Next(server.Hardware.Hdds.Count - 1)];
                ++hddTries;
            }
            if (server.Hardware.Hdds.Count > 1 && !randomHdd.IsMain)
            {
                int previousHddCount = server.Hardware.Hdds.Count;
                var result = client.ServerHdds.Delete(server.Id, randomHdd.Id);

                Thread.Sleep(10000);
                Assert.IsNotNull(result);
            }

        }

        [TestMethod]
        public void GetDVD()
        {
            var random = new Random();
            var servers = client.Servers.Get();
            foreach (var item in servers)
            {
                Thread.Sleep(1000);
                var server = client.Servers.Show(item.Id);
                if (server.DVD != null)
                {
                    var result = client.ServersHardware.ShowDVD(server.Id);

                    Assert.IsNotNull(result);
                    Assert.IsNotNull(result.Id);
                    break;
                }
            }
        }

        [TestMethod]
        public void UpdateDVD()
        {
            var random = new Random();
            var servers = client.Servers.Get();
            var server = servers[random.Next(servers.Count - 1)];
            var dvd = client.DVDs.Get();
            while (server.Status.State == ServerState.REMOVING || server.Status.State == ServerState.CONFIGURING)
            {
                server = servers[random.Next(servers.Count - 1)];
            }
            var result = client.ServersHardware.UpdateDVD(server.Id, dvd[0].id);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteDVD()
        {
            var random = new Random();
            var servers = client.Servers.Get();
            var server = servers[random.Next(servers.Count - 1)];
            while (server.Status.State == ServerState.REMOVING || server.Status.State == ServerState.CONFIGURING)
            {
                server = servers[random.Next(servers.Count - 1)];
            }
            var result = client.ServersHardware.DeleteDVD(server.Id);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);

        }
    }
}
