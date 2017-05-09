using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using System.Threading;
using OneAndOne.POCO.Response.Servers;
using System.Collections.Generic;
using OneAndOne.POCO;

namespace OneAndOne.UnitTests
{
    [TestClass]
    public class ServerHardwareTest
    {

        static OneAndOneClient client = OneAndOneClient.Instance(Config.Configuration);
        static ServerResponse server = null;
        static Hdd hddToUpdate = null;

        [ClassInitialize]
        static public void ServerHardwareInit(TestContext context)
        {
            int vcore = 4;
            int CoresPerProcessor = 2;
            var appliances = client.ServerAppliances.Get(null, null, null, "ubuntu", null);
            POCO.Response.ServerAppliances.ServerAppliancesResponse appliance = null;
            if (appliances == null || appliances.Count() == 0)
            {
                appliance = client.ServerAppliances.Get().FirstOrDefault();
            }
            else
            {
                appliance = appliances.FirstOrDefault();
            }
            var result = client.Servers.Create(new POCO.Requests.Servers.CreateServerRequest()
            {
                ApplianceId = appliance != null ? appliance.Id : null,
                Name = "server hardware test .net",
                Description = "desc",
                Hardware = new POCO.Requests.Servers.HardwareRequest()
                {
                    CoresPerProcessor = CoresPerProcessor,
                    Hdds = new List<POCO.Requests.Servers.HddRequest>()
                        {
                            {new POCO.Requests.Servers.HddRequest()
                            {
                                IsMain=true,
                                Size=20,
                            }},
                            {new POCO.Requests.Servers.HddRequest()
                            {
                                IsMain=false,
                                Size=20,
                            }}
                        },
                    Ram = 4,
                    Vcore = vcore
                },
                PowerOn = true,
            });

            Config.waitServerReady(result.Id);
            server = client.Servers.Show(result.Id);
        }

        [ClassCleanup]
        static public void ServerHardwareClean()
        {
            Config.waitServerReady(server.Id);
            client.Servers.Delete(server.Id, false);
        }

        [TestMethod]
        public void GetServerHardWare()
        {
            var result = client.ServersHardware.Show(server.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.CoresPerProcessor);
        }

        [TestMethod]
        public void UpdateServerHardWare()
        {
            Config.waitServerReady(server.Id);
            //setup initial values for update
            float Ram = 8;
            var request = new POCO.Requests.Servers.UpdateHardwareRequest()
            {
                Ram = (int)Ram,
            };

            Ram = request.Ram.Value;
            var result = client.ServersHardware.Update(request, server.Id);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Hardware.CoresPerProcessor);
            Config.waitServerReady(server.Id);
            var resultserver = client.Servers.Show(result.Id);
            //check if the values are updated as expected
            Assert.AreEqual(resultserver.Hardware.Ram, Ram);
        }

        [TestMethod]
        public void GetServerHardDrives()
        {
            var result = client.ServerHdds.Get(server.Id);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void ShowHardDrives()
        {
            var result = client.ServerHdds.Show(server.Id, server.Hardware.Hdds[0].Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void AddServerHardDrives()
        {
            Config.waitServerReady(server.Id);

            int prevoiousHDDCounts = 0;
            prevoiousHDDCounts = server.Hardware.Hdds.Count;
            var result = client.ServerHdds.Create(new POCO.Requests.Servers.AddHddRequest()
            {
                Hdds = new System.Collections.Generic.List<POCO.Requests.Servers.HddRequest>()
                    {
                        { new POCO.Requests.Servers.HddRequest()
                        {Size=20,IsMain=false}},
                        }
            }, server.Id);
            Config.waitServerReady(server.Id);
            var resultserver = client.Servers.Show(result.Id);
            hddToUpdate = resultserver.Hardware.Hdds.Where(hdd => !hdd.IsMain).FirstOrDefault();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Hardware.Hdds.Count > 0);

            var updateResult = client.ServerHdds.Update(new POCO.Requests.Servers.UpdateHddRequest()
            {
                Size = hddToUpdate.Size + 10
            }, server.Id, hddToUpdate.Id);

            Config.waitServerReady(server.Id);

            Assert.IsNotNull(updateResult);

            //delete HDD
            DeleteHardDrive();
        }

        public void DeleteHardDrive()
        {
            var hdds = client.ServerHdds.Get(server.Id);
            if (hdds.Count > 1)
            {
                var result = client.ServerHdds.Delete(server.Id, hdds.Where(hdd => !hdd.IsMain).FirstOrDefault().Id);
                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        public void GetDVD()
        {
            var serverDvd = client.Servers.Show(server.Id);
            if (serverDvd.DVD != null)
            {
                var result = client.ServersHardware.ShowDVD(serverDvd.Id);

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
            }
        }

        [TestMethod]
        public void UpdateDVD()
        {
            var dvd = client.DVDs.Get(null, null, null, "ubuntu", null);
            Config.waitServerReady(server.Id);
            var result = client.ServersHardware.UpdateDVD(server.Id, dvd[0].Id);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DeleteDVD()
        {
            var result = client.ServersHardware.DeleteDVD(server.Id);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }
    }
}
