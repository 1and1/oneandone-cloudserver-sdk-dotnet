using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Response.MonitoringCenter;
using System.Threading;
using OneAndOne.POCO;
using OneAndOne.POCO.Response.MonitoringPolicies;
using OneAndOne.POCO.Response.Servers;
using System.Collections.Generic;

namespace OneAndOne.UnitTests.MonitoringCenter
{
    [TestClass]
    public class MonitoringCenterTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance(Config.Configuration);
        static MonitoringPoliciesResponse mp = null;
        static ServerResponse server = null;

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
                Name = ".net Monitoring center test",
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

            server = Config.CreateTestServer("Monitoring center servers test");

            Config.waitServerReady(server.Id);
            server = client.Servers.Show(server.Id);

            var servers = new List<string>();
            servers.Add(server.Id);

            var addedServer = client.MonitoringPoliciesServers.Create(servers, mp.Id);
        }

        [ClassCleanup]
        static public void TestClean()
        {
            if (mp != null)
            {
                Config.waitMonitoringPolicyReady(mp.Id);
                client.MonitoringPolicies.Delete(mp.Id);
                client.Servers.Delete(server.Id, false);
            }
        }

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
            var mCenters = client.MonitoringCenter.Show(server.Id, PeriodType.LAST_24H);
            Assert.IsNotNull(mCenters);
            Assert.IsNotNull(mCenters.Id);
        }
    }
}
