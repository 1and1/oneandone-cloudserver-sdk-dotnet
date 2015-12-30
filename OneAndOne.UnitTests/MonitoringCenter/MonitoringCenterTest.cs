using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Respones.MonitoringCenter;
using System.Threading;
using OneAndOne.POCO;

namespace OneAndOne.UnitTests.MonitoringCenter
{
    [TestClass]
    public class MonitoringCenterTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance();

        [TestMethod]
        public void GetMonitoringCenter()
        {
            var mCenters = client.MonitoringCenter.Get();

            Assert.IsNotNull(mCenters);
            Assert.IsTrue(mCenters.Count > 0);
        }

        [TestMethod]
        public void GetServerMonitoringCenter()
        {
            Random random = new Random();
            var servers = client.Servers.Get();
            var server = client.Servers.Show(servers[random.Next(0, servers.Count - 1)].Id);
            foreach (var item in servers)
            {
                Thread.Sleep(500);
                server = client.Servers.Show(item.Id);
                if (server.MonitoringPolicy != null)
                {
                    server = item;
                    break;
                }
            }
            var mCenters = client.MonitoringCenter.Show(server.Id, PeriodType.CUSTOM, DateTime.Today.AddMonths(-2), DateTime.Today);
            Assert.IsNotNull(mCenters);
            Assert.IsNotNull(mCenters.Id);
        }
    }
}
