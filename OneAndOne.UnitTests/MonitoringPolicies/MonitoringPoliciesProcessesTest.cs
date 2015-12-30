using System.Linq;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Requests.MonitoringPolicies;
using System.Collections.Generic;
using OneAndOne.POCO;

namespace OneAndOne.UnitTests.MonitoringPolicies
{
    [TestClass]
    public class MonitoringPoliciesProcessesTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance();
        [TestMethod]
        public void GetMonitoringPolicyProcesss()
        {
            Random random = new Random();
            var monitoringPolicies = client.MonitoringPolicies.Get();
            var monitoringPolicy = monitoringPolicies[random.Next(monitoringPolicies.Count - 1)];
            var result = client.MonitoringPoliciesProcesses.Get(monitoringPolicy.Id);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShowMonitoringPolicyProcess()
        {
            Random random = new Random();
            var monitoringPolicies = client.MonitoringPolicies.Get();
            var monitoringPolicy = monitoringPolicies[random.Next(monitoringPolicies.Count - 1)];
            foreach (var item in monitoringPolicies)
            {
                if (item.Processes != null && item.Processes.Count > 0)
                {
                    monitoringPolicy = item;
                    break;
                }
            }
            if (monitoringPolicy.Processes != null && monitoringPolicy.Processes.Count > 0)
            {
                var processes = client.MonitoringPoliciesProcesses.Get(monitoringPolicy.Id);
                var process = processes[random.Next(processes.Count - 1)];

                var result = client.MonitoringPoliciesProcesses.Show(monitoringPolicy.Id, process.Id);

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
            }
        }

        [TestMethod]
        public void AddMonitoringPolicyProcess()
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
            var processes = new List<POCO.Requests.MonitoringPolicies.Processes>();
            processes.Add(new POCO.Requests.MonitoringPolicies.Processes()
            {
                EmailNotification = true,
                AlertIf = ProcessAlertType.RUNNING,
                Process = "iexplorer"

            });

            processes.Add(new POCO.Requests.MonitoringPolicies.Processes()
            {
                EmailNotification = true,
                AlertIf = ProcessAlertType.RUNNING,
                Process = "test"

            });

            var result = client.MonitoringPoliciesProcesses.Create(processes, monitoringPolicy.Id);


            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);

            //check if Processes created do really exist
            var checkResult = client.MonitoringPolicies.Show(monitoringPolicy.Id);
            Assert.AreEqual(processes.Count, checkResult.Processes.Count);
            foreach (var item in processes)
            {
                var matchingProcess = checkResult.Processes.FirstOrDefault(po => po.Process == item.Process && po.AlertIf == item.AlertIf);
                Assert.AreEqual(item.AlertIf, matchingProcess.AlertIf);
                Assert.AreEqual(item.EmailNotification, matchingProcess.EmailNotification);
            }
        }

        [TestMethod]
        public void UpdateMonitoringPolicyProcess()
        {
            Random random = new Random();
            var monitoringPolicies = client.MonitoringPolicies.Get();
            var monitoringPolicy = monitoringPolicies[random.Next(monitoringPolicies.Count - 1)];
            foreach (var item in monitoringPolicies)
            {
                if (item.Processes != null && item.Processes.Count > 0)
                {
                    monitoringPolicy = item;
                    break;
                }
            }
            if (monitoringPolicy.Processes != null && monitoringPolicy.Processes.Count > 0)
            {
                var result = client.MonitoringPoliciesProcesses.Update(new POCO.Requests.MonitoringPolicies.Processes()
                {
                    EmailNotification = true,
                    AlertIf = ProcessAlertType.RUNNING,
                    Process = "test"

                }, monitoringPolicy.Id, monitoringPolicy.Processes[0].Id);

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
                //check if Processes updated do really exist
                var checkResult = client.MonitoringPolicies.Show(monitoringPolicy.Id);
                Assert.AreEqual(result.Processes.Count, checkResult.Processes.Count);
                foreach (var item in result.Processes)
                {
                    var matchingProcess = checkResult.Processes.FirstOrDefault(po => po.Id == item.Id);
                    Assert.AreEqual(item.AlertIf, matchingProcess.AlertIf);
                    Assert.AreEqual(item.EmailNotification, matchingProcess.EmailNotification);
                }
            }
        }

        [TestMethod]
        public void DeleteMonitoringPolicyProcess()
        {
            Random random = new Random();
            var monitoringPolicies = client.MonitoringPolicies.Get();
            var monitoringPolicy = monitoringPolicies[random.Next(monitoringPolicies.Count - 1)];
            foreach (var item in monitoringPolicies)
            {
                if (item.Processes != null && item.Processes.Count > 0)
                {
                    monitoringPolicy = item;
                    break;
                }
            }
            var result = client.MonitoringPoliciesProcesses.Delete(monitoringPolicy.Id, monitoringPolicy.Processes[0].Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check if Process is removed
            var checkResult = client.MonitoringPolicies.Show(monitoringPolicy.Id);
            Assert.IsFalse(checkResult.Processes.Any(prt => prt.Id == monitoringPolicy.Processes[0].Id));
        }
    }
}
