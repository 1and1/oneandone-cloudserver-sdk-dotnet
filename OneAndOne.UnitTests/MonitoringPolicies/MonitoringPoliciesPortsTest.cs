using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Requests.MonitoringPolicies;
using System.Collections.Generic;
using OneAndOne.POCO;
using OneAndOne.POCO.Response.MonitoringPolicies;

namespace OneAndOne.UnitTests.MonitoringPolicies
{
    [TestClass]
    public class MonitoringPoliciesPortsTest
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
                Name = ".net MP ports test",
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

            var addedPorts = new List<POCO.Requests.MonitoringPolicies.Ports>();
            addedPorts.Add(new POCO.Requests.MonitoringPolicies.Ports()
            {
                EmailNotification = true,
                AlertIf = AlertIfType.RESPONDING,
                Port = 97,
                Protocol = ProtocolType.TCP

            });

            addedPorts.Add(new POCO.Requests.MonitoringPolicies.Ports()
            {
                EmailNotification = true,
                AlertIf = AlertIfType.RESPONDING,
                Port = 98,
                Protocol = ProtocolType.TCP

            });

            updatedMp = client.MonitoringPoliciesPorts.Create(addedPorts, mp.Id);


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
                DeleteMonitoringPolicyPort();
                Config.waitMonitoringPolicyReady(mp.Id);
                client.MonitoringPolicies.Delete(mp.Id);
            }
        }
        [TestMethod]
        public void GetMonitoringPolicyPorts()
        {
            var result = client.MonitoringPoliciesPorts.Get(mp.Id);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShowMonitoringPolicyPort()
        {
            var result = client.MonitoringPoliciesPorts.Show(mp.Id, updatedMp.Ports[0].Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void UpdateMonitoringPolicyport()
        {
            var request = new POCO.Requests.MonitoringPolicies.Ports()
            {
                EmailNotification = true,
                AlertIf = AlertIfType.RESPONDING,
                Port = 23,
                Protocol = ProtocolType.TCP

            };
            var result = client.MonitoringPoliciesPorts.Update(request, mp.Id, updatedMp.Ports[0].Id);


            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check if ports updated do really exist
            var checkResult = client.MonitoringPolicies.Show(mp.Id);
            Assert.AreEqual(result.Ports.Count, checkResult.Ports.Count);
            foreach (var item in result.Ports)
            {
                var matchingPort = checkResult.Ports.FirstOrDefault(po => po.Id == item.Id);
                Assert.AreEqual(item.AlertIf, matchingPort.AlertIf);
                Assert.AreEqual(item.EmailNotification, matchingPort.EmailNotification);
            }

        }

        static public void DeleteMonitoringPolicyPort()
        {
            var result = client.MonitoringPoliciesPorts.Delete(mp.Id, updatedMp.Ports[0].Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check if ports is removed
            var checkResult = client.MonitoringPolicies.Show(updatedMp.Id);
            Assert.IsFalse(checkResult.Ports.Any(prt => prt.Id == updatedMp.Ports[0].Id));
        }
    }
}
