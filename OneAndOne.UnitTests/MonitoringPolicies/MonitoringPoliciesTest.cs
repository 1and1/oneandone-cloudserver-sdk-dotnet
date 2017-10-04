using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using System.Collections.Generic;
using OneAndOne.POCO.Requests.MonitoringPolicies;
using OneAndOne.POCO;
using System.Threading;
using OneAndOne.POCO.Response.MonitoringPolicies;

namespace OneAndOne.UnitTests.MonitoringPolicies
{
    [TestClass]
    public class MonitoringPoliciesTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance(Config.Configuration);
        static MonitoringPoliciesResponse mp = null;

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
                Name = ".net MP test" ,
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

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            var monitoringPolicyResult = client.MonitoringPolicies.Show(result.Id);
            mp = monitoringPolicyResult;
            Assert.AreEqual(monitoringPolicyResult.Agent, request.Agent);
            Assert.AreEqual(monitoringPolicyResult.Ports.Count, request.Ports.Count);
            Assert.AreEqual(monitoringPolicyResult.Processes.Count, request.Processes.Count);
            //check CPU values
            Assert.AreEqual(monitoringPolicyResult.Thresholds.Cpu.Critical.Alert, request.Thresholds.Cpu.Critical.Alert);
            Assert.AreEqual(monitoringPolicyResult.Thresholds.Cpu.Critical.Value, request.Thresholds.Cpu.Critical.Value);
            Assert.AreEqual(monitoringPolicyResult.Thresholds.Cpu.Warning.Alert, request.Thresholds.Cpu.Warning.Alert);
            Assert.AreEqual(monitoringPolicyResult.Thresholds.Cpu.Warning.Value, request.Thresholds.Cpu.Warning.Value);
            //check RAM values
            Assert.AreEqual(monitoringPolicyResult.Thresholds.Ram.Critical.Alert, request.Thresholds.Ram.Critical.Alert);
            Assert.AreEqual(monitoringPolicyResult.Thresholds.Ram.Critical.Value, request.Thresholds.Ram.Critical.Value);
            Assert.AreEqual(monitoringPolicyResult.Thresholds.Ram.Warning.Alert, request.Thresholds.Ram.Warning.Alert);
            Assert.AreEqual(monitoringPolicyResult.Thresholds.Ram.Warning.Value, request.Thresholds.Ram.Warning.Value);
            //check InternalPing values
            Assert.AreEqual(monitoringPolicyResult.Thresholds.InternalPing.Critical.Alert, request.Thresholds.InternalPing.Critical.Alert);
            Assert.AreEqual(monitoringPolicyResult.Thresholds.InternalPing.Critical.Value, request.Thresholds.InternalPing.Critical.Value);
            Assert.AreEqual(monitoringPolicyResult.Thresholds.InternalPing.Warning.Alert, request.Thresholds.InternalPing.Warning.Alert);
            Assert.AreEqual(monitoringPolicyResult.Thresholds.InternalPing.Warning.Value, request.Thresholds.InternalPing.Warning.Value);
            //check Transfer values
            Assert.AreEqual(monitoringPolicyResult.Thresholds.Transfer.Critical.Alert, request.Thresholds.Transfer.Critical.Alert);
            Assert.AreEqual(monitoringPolicyResult.Thresholds.Transfer.Critical.Value, request.Thresholds.Transfer.Critical.Value);
            Assert.AreEqual(monitoringPolicyResult.Thresholds.Transfer.Warning.Alert, request.Thresholds.Transfer.Warning.Alert);
            Assert.AreEqual(monitoringPolicyResult.Thresholds.Transfer.Warning.Value, request.Thresholds.Transfer.Warning.Value);
            Config.waitMonitoringPolicyReady(mp.Id);
        }

        [ClassCleanup]
        static public void TestClean()
        {
            if (mp != null)
            {
                Config.waitMonitoringPolicyReady(mp.Id);
                DeleteMonitoringPolicy();
            }
        }


        [TestMethod]
        public void GetMonitoringPolicies()
        {
            var result = client.MonitoringPolicies.Get();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShowMonitoringPolicy()
        {
            var result = client.MonitoringPolicies.Show(mp.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void UpdateMonitoringPolicy()
        {
            var request = new UpdateMonitoringPolicyRequest()
            {
                Name = "updated name",
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
            var result = client.MonitoringPolicies.Update(request, mp.Id);
            Config.waitMonitoringPolicyReady(mp.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            var monitoringPolicyResult = client.MonitoringPolicies.Show(result.Id);
            //check CPU values
            Assert.AreEqual(monitoringPolicyResult.Thresholds.Cpu.Critical.Alert, request.Thresholds.Cpu.Critical.Alert);
            Assert.AreEqual(monitoringPolicyResult.Thresholds.Cpu.Critical.Value, request.Thresholds.Cpu.Critical.Value);
            Assert.AreEqual(monitoringPolicyResult.Thresholds.Cpu.Warning.Alert, request.Thresholds.Cpu.Warning.Alert);
            Assert.AreEqual(monitoringPolicyResult.Thresholds.Cpu.Warning.Value, request.Thresholds.Cpu.Warning.Value);
            //check RAM values
            Assert.AreEqual(monitoringPolicyResult.Thresholds.Ram.Critical.Alert, request.Thresholds.Ram.Critical.Alert);
            Assert.AreEqual(monitoringPolicyResult.Thresholds.Ram.Critical.Value, request.Thresholds.Ram.Critical.Value);
            Assert.AreEqual(monitoringPolicyResult.Thresholds.Ram.Warning.Alert, request.Thresholds.Ram.Warning.Alert);
            Assert.AreEqual(monitoringPolicyResult.Thresholds.Ram.Warning.Value, request.Thresholds.Ram.Warning.Value);
            //check InternalPing values
            Assert.AreEqual(monitoringPolicyResult.Thresholds.InternalPing.Critical.Alert, request.Thresholds.InternalPing.Critical.Alert);
            Assert.AreEqual(monitoringPolicyResult.Thresholds.InternalPing.Critical.Value, request.Thresholds.InternalPing.Critical.Value);
            Assert.AreEqual(monitoringPolicyResult.Thresholds.InternalPing.Warning.Alert, request.Thresholds.InternalPing.Warning.Alert);
            Assert.AreEqual(monitoringPolicyResult.Thresholds.InternalPing.Warning.Value, request.Thresholds.InternalPing.Warning.Value);
            //check Transfer values
            Assert.AreEqual(monitoringPolicyResult.Thresholds.Transfer.Critical.Alert, request.Thresholds.Transfer.Critical.Alert);
            Assert.AreEqual(monitoringPolicyResult.Thresholds.Transfer.Critical.Value, request.Thresholds.Transfer.Critical.Value);
            Assert.AreEqual(monitoringPolicyResult.Thresholds.Transfer.Warning.Alert, request.Thresholds.Transfer.Warning.Alert);
            Assert.AreEqual(monitoringPolicyResult.Thresholds.Transfer.Warning.Value, request.Thresholds.Transfer.Warning.Value);
        }

        static public void DeleteMonitoringPolicy()
        {
            var result = client.MonitoringPolicies.Delete(mp.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check if the monitoring policy is removed
            var monitoringPoliciesResult = client.MonitoringPolicies.Get();
            Assert.IsFalse(monitoringPoliciesResult.Any(mp => mp.Id == result.Id));
        }
    }
}
