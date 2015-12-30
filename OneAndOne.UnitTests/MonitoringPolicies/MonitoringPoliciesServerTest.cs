using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using System.Collections.Generic;

namespace OneAndOne.UnitTests.MonitoringPolicies
{
    [TestClass]
    public class MonitoringPoliciesServerTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance();
        [TestMethod]
        public void GetMonitoringPolicyServers()
        {
            Random random = new Random();
            var monitoringPolicies = client.MonitoringPolicies.Get();
            var monitoringPolicy = monitoringPolicies[random.Next(monitoringPolicies.Count - 1)];
            var result = client.MonitoringPoliciesServers.Get(monitoringPolicy.Id);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShowMonitoringPolicyServer()
        {
            Random random = new Random();
            var monitoringPolicies = client.MonitoringPolicies.Get();
            var monitoringPolicy = monitoringPolicies[random.Next(monitoringPolicies.Count - 1)];
            foreach (var item in monitoringPolicies)
            {
                if (item.Servers != null && item.Servers.Count > 0)
                {
                    monitoringPolicy = item;
                    break;
                }
            }
            if (monitoringPolicy.Servers != null && monitoringPolicy.Servers.Count > 0)
            {
                var servers = client.MonitoringPoliciesServers.Get(monitoringPolicy.Id);
                var server = servers[random.Next(servers.Count - 1)];

                var result = client.MonitoringPoliciesServers.Show(monitoringPolicy.Id, server.Id);

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
            }
        }

        [TestMethod]
        public void AddMonitoringPolicyServer()
        {
            Random random = new Random();
            var monitoringPolicies = client.MonitoringPolicies.Get();
            var monitoringPolicy = monitoringPolicies[random.Next(monitoringPolicies.Count - 1)];
            foreach (var item in monitoringPolicies)
            {
                if (item.Default < 1)
                {
                    monitoringPolicy = item;
                    break;
                }
            }
            if (monitoringPolicy.Default == 1)
                return;
            var serversList = client.Servers.Get();
            var servers = new List<string>();
            servers.Add(serversList[0].Id);
            if (servers.Count > 1)
            {
                servers.Add(serversList[1].Id);
            }

            var result = client.MonitoringPoliciesServers.Create(servers, monitoringPolicy.Id);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check if servers created do really exist
            var checkResult = client.MonitoringPolicies.Show(monitoringPolicy.Id);
            Assert.AreEqual(servers.Count, checkResult.Servers.Count);
            foreach (var item in servers)
            {
                var matchingServer = checkResult.Servers.FirstOrDefault(po => po.Id == item);
                Assert.AreEqual(item, matchingServer.Id);
            }
        }

        [TestMethod]
        public void DeleteMonitoringPolicyServer()
        {
            Random random = new Random();
            var monitoringPolicies = client.MonitoringPolicies.Get();
            var monitoringPolicy = monitoringPolicies[random.Next(monitoringPolicies.Count - 1)];
            foreach (var item in monitoringPolicies)
            {
                if (item.Servers != null && item.Servers.Count > 0)
                {
                    monitoringPolicy = item;
                    break;
                }
            }
            var result = client.MonitoringPoliciesServers.Delete(monitoringPolicy.Id, monitoringPolicy.Servers[0].Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check if server is removed
            var checkResult = client.MonitoringPolicies.Show(monitoringPolicy.Id);
            Assert.IsFalse(checkResult.Servers.Any(ser => ser.Id == monitoringPolicy.Servers[0].Id));
        }
    }
}
