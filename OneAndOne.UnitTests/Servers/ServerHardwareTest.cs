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

        static OneAndOneClient client = OneAndOneClient.Instance();

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
            var servers = client.Servers.Get().Where(ser => ser.Name.Contains("ServerTest")).ToList();
            var server = servers[random.Next(servers.Count - 1)];
            //setup initial values for update
            int CoresPerProcessor = 4;
            float Ram = 4;
            int Vcore = 4;
            //search for an appropriate server for udpating
            foreach (var item in servers)
            {
                server = client.Servers.Show(item.Id);
                //cannot update servers with snapshots 
                //cannot hot update servers with linux system installed
                if (server.Status.State == ServerState.DEPLOYING || server.Snapshot != null || server.Image.Name.Contains("ub") || server.Image.Name.Contains("centos"))
                {
                    continue;
                }
                //check if server current values are less than the updated values for hot update
                if (server.Hardware.CoresPerProcessor < CoresPerProcessor && server.Hardware.Ram < Ram && server.Hardware.Vcore < Vcore)
                {
                    server = item;
                    break;
                }
                //else increase the already existing values
                else
                {
                    CoresPerProcessor = server.Hardware.CoresPerProcessor + 1;
                    Ram = server.Hardware.Ram + (float)2.0;
                    Vcore = server.Hardware.Vcore + 2;
                    break;
                }
            }
            //if the updated values exceed any of the limits stop the update
            CoresPerProcessor = server.Hardware.CoresPerProcessor + 1;
            Ram = server.Hardware.Ram + (float)2.0;
            Vcore = server.Hardware.Vcore + 2;
            if (CoresPerProcessor > 16 || Ram > 128 || Vcore > 16 || CoresPerProcessor > Vcore)
            {
                return;
            }
            if (server.Status.State != ServerState.DEPLOYING && server.Snapshot == null && !server.Image.Name.Contains("ub") && !server.Image.Name.Contains("centos"))
            {

                var request = new POCO.Requests.Servers.UpdateHardwareRequest()
                    {
                        CoresPerProcessor = server.Hardware.Vcore > 1 ? server.Hardware.Vcore / 2 : 1,
                        Ram = (int)Ram,
                        Vcore = server.Hardware.Vcore
                    };

                CoresPerProcessor = request.CoresPerProcessor;
                Vcore = request.Vcore;
                Ram = request.Ram;
                var result = client.ServersHardware.Update(request, server.Id);
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Hardware.CoresPerProcessor);
                //give the server time to update
                var resultserver = client.Servers.Show(result.Id);
                while (resultserver.Status.Percent > 0)
                {
                    Thread.Sleep(5000);
                    resultserver = client.Servers.Show(result.Id);
                }
                //check if the values are updated as expected
                Assert.AreEqual(resultserver.Hardware.CoresPerProcessor, CoresPerProcessor);
                Assert.AreEqual(resultserver.Hardware.Ram, Ram);
                Assert.AreEqual(resultserver.Hardware.Vcore, Vcore);
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
            var servers = client.Servers.Get().Where(ser => ser.Name.Contains("ServerTest") || ser.Name.Contains("Updated")).ToList();
            var server = servers[random.Next(servers.Count - 1)];
            int prevoiousHDDCounts = 0;
            foreach (var item in servers)
            {
                Thread.Sleep(1000);
                server = client.Servers.Show(item.Id);
                if (server.Snapshot == null)
                {
                    break;
                }
            }
            if (server.Hardware.Hdds.Count < 8 && server.Snapshot == null && server.Status.State != ServerState.DEPLOYING)
            {
                prevoiousHDDCounts = server.Hardware.Hdds.Count;
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
                Thread.Sleep(8000);
                var resultserver = client.Servers.Show(result.Id);
                while (resultserver.Hardware.Hdds.Count == prevoiousHDDCounts)
                {
                    Thread.Sleep(1000);
                    resultserver = client.Servers.Show(result.Id);
                }
                Assert.IsNotNull(result);
                Assert.IsTrue(result.Hardware.Hdds.Count > 0);
                //check if the number of HDD has increased 
                Assert.IsTrue(resultserver.Hardware.Hdds.Count > prevoiousHDDCounts);
            }
        }

        [TestMethod]
        public void UpdateHardDrives()
        {
            var random = new Random();
            var servers = client.Servers.Get().Where(ser => ser.Name.Contains("ServerTest") || ser.Name.Contains("Updated")).ToList(); ;
            var server = servers[random.Next(servers.Count - 1)];
            var randomHdd = server.Hardware.Hdds[random.Next(server.Hardware.Hdds.Count - 1)];
            int previousSize = randomHdd.Size;
            if (server.Status.State == ServerState.REMOVING || server.Status.State == ServerState.DEPLOYING
                || server.Status.State == ServerState.CONFIGURING || server.Status.State == ServerState.CONFIGURING)
            {
                return;
            }
            int updatedSize = 20;
            if (randomHdd.Size < 100)
                updatedSize = 120;
            else
            {
                updatedSize = randomHdd.Size + 20;
            }
            if (randomHdd.Size == 2000 || randomHdd.Size > updatedSize)
            {
                return;
            }
            var result = client.ServerHdds.Update(new POCO.Requests.Servers.UpdateHddRequest()
                {
                    Size = updatedSize
                }, server.Id, randomHdd.Id);

            Assert.IsNotNull(result);
            //check if the number of HDD size increased 
            Assert.IsTrue(updatedSize > previousSize);
        }

        [TestMethod]
        public void DeleteHardDrive()
        {
            var random = new Random();
            var servers = client.Servers.Get();
            var server = servers[random.Next(servers.Count - 1)];
            var randomHdd = server.Hardware.Hdds[random.Next(server.Hardware.Hdds.Count - 1)];
            foreach (var item in servers)
            {
                Thread.Sleep(1000);
                server = client.Servers.Show(item.Id);
                if (server.Hardware.Hdds.Count > 1 && server.Status.State != ServerState.REMOVING && server.Snapshot == null)
                {
                    server = item;
                    break;
                }
            }
            foreach (var item in server.Hardware.Hdds)
            {
                if (!item.IsMain)
                {
                    randomHdd = item;
                    break;
                }
            }

            if (server.Hardware.Hdds.Count > 1 && !randomHdd.IsMain)
            {
                string previousHddId = randomHdd.Id;
                var result = client.ServerHdds.Delete(server.Id, randomHdd.Id);
                Assert.IsNotNull(result);
                //check if the number of HDD number decreased 
                var resultserver = client.Servers.Show(result.Id);
                while (resultserver.Hardware.Hdds.Any(Hdd => Hdd.Id == previousHddId))
                {
                    Thread.Sleep(1000);
                    resultserver = client.Servers.Show(result.Id);
                }
                Thread.Sleep(1000);
                resultserver = client.Servers.Show(result.Id);
                Assert.IsFalse(resultserver.Hardware.Hdds.Any(Hdd => Hdd.Id == previousHddId));
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
            while (server.Status.State == ServerState.REMOVING || server.Status.State == ServerState.DEPLOYING
                || server.Status.State == ServerState.CONFIGURING || server.Status.State == ServerState.CONFIGURING)
            {
                server = servers[random.Next(servers.Count - 1)];
            }
            var result = client.ServersHardware.UpdateDVD(server.Id, dvd[0].Id);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteDVD()
        {
            var random = new Random();
            var servers = client.Servers.Get();
            var server = servers[random.Next(servers.Count - 1)];
            while (server.Status.State == ServerState.REMOVING || server.Status.State == ServerState.DEPLOYING
                || server.Status.State == ServerState.CONFIGURING || server.Status.State == ServerState.CONFIGURING)
            {
                server = servers[random.Next(servers.Count - 1)];
            }
            var result = client.ServersHardware.DeleteDVD(server.Id);
            //give the server time to update
            var resultserver = client.ServersHardware.ShowDVD(result.Id);
            while (resultserver != null)
            {
                Thread.Sleep(5000);
                resultserver = client.ServersHardware.ShowDVD(result.Id);
            }
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            Assert.IsNull(resultserver);

        }
    }
}
