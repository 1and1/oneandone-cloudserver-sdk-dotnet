using System.Linq;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Requests.MonitoringPolicies;
using System.Collections.Generic;
using OneAndOne.POCO;
using OneAndOne.POCO.Response.MonitoringPolicies;

namespace OneAndOne.UnitTests.MonitoringPolicies
{
    [TestClass]
    public class MonitoringPoliciesProcessesTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance(Config.Configuration);
        static MonitoringPoliciesResponse mp = null;
        static MonitoringPoliciesResponse updatedMp = null;

        [ClassInitialize]
        static public void TestInit(TestContext context)
        {
            var ports = new List<POCO.Requests.MonitoringPolicies.Ports>();
            ports.Add(new POCO.Requests.MonitoringPolicies.Ports()
            {
                EmailNotification = true,
                AlertIf = AlertIfType.RESPONDING,
                Port = 22,
                Protocol = ProtocolType.TCP
            });
            var processes = new List<POCO.Requests.MonitoringPolicies.Processes>();
            processes.Add(new POCO.Requests.MonitoringPolicies.Processes()
            {
                EmailNotification = true,
                AlertIf = ProcessAlertType.NOT_RUNNING,
                Process = "test",
            });
            var request = new POCO.Requests.MonitoringPolicies.CreateMonitoringPolicyRequest()
            {
                Name = ".net MP Process test",
                Description = ".net decription",
                Agent = true,
                Ports = ports,
                Processes = processes,
                Thresholds = new POCO.Requests.MonitoringPolicies.Thresholds()
                {
                    Cpu = new POCO.Requests.MonitoringPolicies.Cpu()
                    {
                        Critical = new POCO.Requests.MonitoringPolicies.Critical()
                        {
                            Alert = false,
                            Value = 95
                        },
                        Warning = new POCO.Requests.MonitoringPolicies.Warning()
                        {
                            Alert = false,
                            Value = 90
                        }
                    },
                    Ram = new POCO.Requests.MonitoringPolicies.Ram()
                    {
                        Critical = new POCO.Requests.MonitoringPolicies.Critical()
                        {
                            Alert = false,
                            Value = 95
                        },
                        Warning = new POCO.Requests.MonitoringPolicies.Warning()
                        {
                            Alert = false,
                            Value = 90
                        }
                    },
                    Disk = new POCO.Requests.MonitoringPolicies.Disk()
                    {
                        Critical = new POCO.Requests.MonitoringPolicies.DiskCritical()
                        {
                            Alert = false,
                            Value = 90
                        },
                        Warning = new POCO.Requests.MonitoringPolicies.DiskWarning()
                        {
                            Alert = false,
                            Value = 80
                        }
                    },
                    InternalPing = new POCO.Requests.MonitoringPolicies.InternalPing()
                    {
                        Critical = new POCO.Requests.MonitoringPolicies.InternalPingCritical()
                        {
                            Alert = false,
                            Value = 100
                        },
                        Warning = new POCO.Requests.MonitoringPolicies.InternalPingWarning()
                        {
                            Alert = false,
                            Value = 50
                        }
                    },
                    Transfer = new POCO.Requests.MonitoringPolicies.Transfer()
                    {
                        Critical = new POCO.Requests.MonitoringPolicies.TransferCritical()
                        {
                            Alert = false,
                            Value = 2000
                        },
                        Warning = new POCO.Requests.MonitoringPolicies.Warning()
                        {
                            Alert = false,
                            Value = 1000
                        }
                    }
                }
            };
            var result = client.MonitoringPolicies.Create(request);
            mp = result;

            Config.waitMonitoringPolicyReady(mp.Id);

            var newProcesses = new List<POCO.Requests.MonitoringPolicies.Processes>();
            newProcesses.Add(new POCO.Requests.MonitoringPolicies.Processes()
            {
                EmailNotification = true,
                AlertIf = ProcessAlertType.RUNNING,
                Process = "iexplorer"

            });

            newProcesses.Add(new POCO.Requests.MonitoringPolicies.Processes()
            {
                EmailNotification = true,
                AlertIf = ProcessAlertType.RUNNING,
                Process = "test"

            });

            updatedMp = client.MonitoringPoliciesProcesses.Create(newProcesses, mp.Id);


            Assert.IsNotNull(updatedMp);
            Assert.IsNotNull(updatedMp.Id);

            Config.waitMonitoringPolicyReady(mp.Id);

        }

        [ClassCleanup]
        static public void TestClean()
        {
            if (mp != null)
            {
                Config.waitMonitoringPolicyReady(mp.Id);
                DeleteMonitoringPolicyProcess();
                Config.waitMonitoringPolicyReady(mp.Id);
                client.MonitoringPolicies.Delete(mp.Id);
            }
        }

        [TestMethod]
        public void GetMonitoringPolicyProcesss()
        {
            var result = client.MonitoringPoliciesProcesses.Get(mp.Id);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShowMonitoringPolicyProcess()
        {
            var processes = client.MonitoringPoliciesProcesses.Get(mp.Id);
            var result = client.MonitoringPoliciesProcesses.Show(mp.Id, processes[0].Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void UpdateMonitoringPolicyProcess()
        {
            Config.waitMonitoringPolicyReady(mp.Id);

            var result = client.MonitoringPoliciesProcesses.Update(new POCO.Requests.MonitoringPolicies.Processes()
            {
                EmailNotification = false,
                AlertIf = ProcessAlertType.RUNNING,
                Process = "iexplorer"

            }, mp.Id, updatedMp.Processes[0].Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check if Processes updated do really exist
            var checkResult = client.MonitoringPolicies.Show(mp.Id);
            Assert.AreEqual(result.Processes.Count, checkResult.Processes.Count);
            foreach (var item in result.Processes)
            {
                var matchingProcess = checkResult.Processes.FirstOrDefault(po => po.Id == item.Id);
                Assert.AreEqual(item.AlertIf, matchingProcess.AlertIf);
                Assert.AreEqual(item.EmailNotification, matchingProcess.EmailNotification);
            }
        }

        static public void DeleteMonitoringPolicyProcess()
        {

            var result = client.MonitoringPoliciesProcesses.Delete(updatedMp.Id, updatedMp.Processes[0].Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check if Process is removed
            var checkResult = client.MonitoringPolicies.Show(updatedMp.Id);
            Assert.IsFalse(checkResult.Processes.Any(prt => prt.Id == updatedMp.Processes[0].Id));
        }
    }
}
