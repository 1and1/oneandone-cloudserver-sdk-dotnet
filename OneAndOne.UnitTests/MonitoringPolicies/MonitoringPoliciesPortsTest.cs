using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Requests.MonitoringPolicies;
using System.Collections.Generic;
using OneAndOne.POCO;

namespace OneAndOne.UnitTests.MonitoringPolicies
{
    [TestClass]
    public class MonitoringPoliciesPortsTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance();
        [TestMethod]
        public void GetMonitoringPolicyPorts()
        {
            Random random = new Random();
            var monitoringPolicies = client.MonitoringPolicies.Get();
            var monitoringPolicy = monitoringPolicies[random.Next(0, monitoringPolicies.Count - 1)];
            var result = client.MonitoringPoliciesPorts.Get(monitoringPolicy.Id);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShowMonitoringPolicyPort()
        {
            Random random = new Random();
            var monitoringPolicies = client.MonitoringPolicies.Get();
            var monitoringPolicy = monitoringPolicies[random.Next(monitoringPolicies.Count - 1)];
            foreach (var item in monitoringPolicies)
            {
                if (item.Ports != null && item.Ports.Count > 0)
                {
                    monitoringPolicy = item;
                    break;
                }
            }
            if (monitoringPolicy.Ports != null && monitoringPolicy.Ports.Count > 0)
            {
                var ports = client.MonitoringPoliciesPorts.Get(monitoringPolicy.Id);
                var port = ports[random.Next(ports.Count - 1)];

                var result = client.MonitoringPoliciesPorts.Show(monitoringPolicy.Id, port.Id);

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
            }
        }

        [TestMethod]
        public void AddMonitoringPolicyPort()
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
            var ports = new List<POCO.Requests.MonitoringPolicies.Ports>();
            ports.Add(new POCO.Requests.MonitoringPolicies.Ports()
                {
                    EmailNotification = true,
                    AlertIf = AlertIfType.RESPONDING,
                    Port = 97,
                    Protocol = ProtocolType.TCP

                });

            ports.Add(new POCO.Requests.MonitoringPolicies.Ports()
                {
                    EmailNotification = true,
                    AlertIf = AlertIfType.RESPONDING,
                    Port = 98,
                    Protocol = ProtocolType.TCP

                });

            var result = client.MonitoringPoliciesPorts.Create(ports, monitoringPolicy.Id);


            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check if ports created do really exist
            var checkResult = client.MonitoringPolicies.Show(monitoringPolicy.Id);
            Assert.AreEqual(ports.Count, checkResult.Ports.Count);
            foreach (var item in ports)
            {
                var matchingPort = checkResult.Ports.FirstOrDefault(po => po.Protocol == item.Protocol && po.Port == item.Port);
                Assert.AreEqual(item.AlertIf, matchingPort.AlertIf);
                Assert.AreEqual(item.EmailNotification, matchingPort.EmailNotification);
            }
        }

        [TestMethod]
        public void UpdateMonitoringPolicyport()
        {
            Random random = new Random();
            var monitoringPolicies = client.MonitoringPolicies.Get();
            var monitoringPolicy = monitoringPolicies[random.Next(monitoringPolicies.Count - 1)];
            foreach (var item in monitoringPolicies)
            {
                if (item.Ports != null && item.Ports.Count > 0)
                {
                    monitoringPolicy = item;
                    break;
                }
            }
            if (monitoringPolicy.Ports != null && monitoringPolicy.Ports.Count > 0)
            {
                var request = new POCO.Requests.MonitoringPolicies.Ports()
                {
                    EmailNotification = true,
                    AlertIf = AlertIfType.RESPONDING,
                    Port = 23,
                    Protocol = ProtocolType.TCP

                };
                var result = client.MonitoringPoliciesPorts.Update(request, monitoringPolicy.Id, monitoringPolicy.Ports[0].Id);


                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
                //check if ports updated do really exist
                var checkResult = client.MonitoringPolicies.Show(monitoringPolicy.Id);
                Assert.AreEqual(result.Ports.Count, checkResult.Ports.Count);
                foreach (var item in result.Ports)
                {
                    var matchingPort = checkResult.Ports.FirstOrDefault(po => po.Id == item.Id);
                    Assert.AreEqual(item.AlertIf, matchingPort.AlertIf);
                    Assert.AreEqual(item.EmailNotification, matchingPort.EmailNotification);
                }

            }
        }

        [TestMethod]
        public void DeleteMonitoringPolicyPort()
        {
            Random random = new Random();
            var monitoringPolicies = client.MonitoringPolicies.Get();
            var monitoringPolicy = monitoringPolicies[random.Next(monitoringPolicies.Count - 1)];
            foreach (var item in monitoringPolicies)
            {
                if (item.Ports != null && item.Ports.Count > 0)
                {
                    monitoringPolicy = item;
                    break;
                }
            }
            var result = client.MonitoringPoliciesPorts.Delete(monitoringPolicy.Id, monitoringPolicy.Ports[0].Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check if ports is removed
            var checkResult = client.MonitoringPolicies.Show(monitoringPolicy.Id);
            Assert.IsFalse(checkResult.Ports.Any(prt => prt.Id == monitoringPolicy.Ports[0].Id));
        }
    }
}
