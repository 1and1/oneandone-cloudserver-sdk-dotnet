using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using System.Collections.Generic;
using OneAndOne.POCO.Response.MonitoringPolicies;
using OneAndOne.POCO;
using OneAndOne.POCO.Response.Servers;

namespace OneAndOne.UnitTests.MonitoringPolicies
{
    [TestClass]
    public class MonitoringPoliciesServerTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance(Config.Configuration);
        static MonitoringPoliciesResponse mp = null;
        static List<String> serversList = new List<string>();
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
                Name = ".net MP test",
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

            server = Config.CreateTestServer("mp servers test");

            Config.waitServerReady(server.Id);
            server = client.Servers.Show(server.Id);

            var servers = new List<string>();
            servers.Add(server.Id);

            var addedServer = client.MonitoringPoliciesServers.Create(servers, mp.Id);
            Assert.IsNotNull(addedServer);
            Assert.IsNotNull(addedServer.Id);
            //check if servers created do really exist
            var checkResult = client.MonitoringPolicies.Show(mp.Id);
            Assert.AreEqual(servers.Count, checkResult.Servers.Count);
            foreach (var item in servers)
            {
                var matchingServer = checkResult.Servers.FirstOrDefault(po => po.Id == item);
                Assert.AreEqual(item, matchingServer.Id);
            }

        }

        [ClassCleanup]
        static public void TestClean()
        {
            if (mp != null)
            {
                Config.waitMonitoringPolicyReady(mp.Id);
                DeleteMonitoringPolicyServer();
                Config.waitMonitoringPolicyReady(mp.Id);
                client.MonitoringPolicies.Delete(mp.Id);
                client.Servers.Delete(server.Id, false);
            }
        }
        [TestMethod]
        public void GetMonitoringPolicyServers()
        {
            var result = client.MonitoringPoliciesServers.Get(mp.Id);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShowMonitoringPolicyServer()
        {
            var result = client.MonitoringPoliciesServers.Show(mp.Id, server.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        static public void DeleteMonitoringPolicyServer()
        {
            var result = client.MonitoringPoliciesServers.Delete(mp.Id, server.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check if server is removed
            Config.waitMonitoringPolicyReady(mp.Id);
            var checkResult = client.MonitoringPolicies.Show(mp.Id);
            Assert.IsFalse(checkResult.Servers.Any(ser => ser.Id == server.Id));
        }
    }
}
