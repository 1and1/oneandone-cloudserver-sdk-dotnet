using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Requests.Servers;
using OneAndOne.POCO.Response;
using OneAndOne.POCO.Response.LoadBalancers;
using OneAndOne.POCO.Response.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OneAndOne.UnitTests
{
    [TestClass]
    public class ServerIpsTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance(Config.Configuration);
        Random random = new Random();
        static ServerResponse server = null;
        static LoadBalancerResponse loadBalancer = null;
        static POCO.Response.FirewallPolicyResponse firewallPolicy = null;

        [ClassInitialize]
        static public void ServerInit(TestContext context)
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
                appliance = appliances.FirstOrDefault(ap=>ap.Type=="IMAGE");
            }
            var result = client.Servers.Create(new POCO.Requests.Servers.CreateServerRequest()
            {
                ApplianceId = appliance != null ? appliance.Id : null,
                Name = "server ip test .net1",
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
        static public void ServerClean()
        {
            Config.waitServerReady(server.Id);
            DeleteIP();
            Config.waitServerReady(server.Id);
            client.Servers.Delete(server.Id, false);
            Config.waitServerDeleted(server.Id);

            if (loadBalancer != null)
            {
                Config.waitLoadBalancerReady(loadBalancer.Id);
                client.LoadBalancer.Delete(loadBalancer.Id);
            }

            if (firewallPolicy != null)
            {
                Config.waitFirewallPolicyReady(firewallPolicy.Id);
                client.FirewallPolicies.Delete(firewallPolicy.Id);

            }

        }
        [TestMethod]
        public void GetServerIPList()
        {
            Config.waitServerReady(server.Id);
            var result = client.ServerIps.Get(server.Id);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void AddServerIP()
        {
            int previousIpCount = 0;
            Config.waitServerReady(server.Id);

            var result = client.ServerIps.Create(new POCO.Requests.Servers.CreateServerIPRequest()
            {
                Type = IPType.Ipv4
            }, server.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);

            Config.waitServerReady(server.Id);
            //give the server time to update
            var resultserver = client.Servers.Show(result.Id);
            while (resultserver.Ips.Count == previousIpCount)
            {
                Thread.Sleep(2000);
                resultserver = client.Servers.Show(result.Id);
            }
            Assert.IsNotNull(resultserver.Ips.Count > previousIpCount);

            Config.waitServerReady(server.Id);
        }

        [TestMethod]
        public void ShowIP()
        {
            Config.waitServerReady(server.Id);
            var result = client.ServerIps.Show(server.Id, server.Ips[0].Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        static public void DeleteIP()
        {
            var serverWithips = client.Servers.Show(server.Id);
            if (serverWithips.Ips != null && serverWithips.Ips.Count > 1)
            {
                Config.waitServerReady(server.Id);
                Config.waitIpReady(serverWithips.Ips[1].Id);
                var result = client.ServerIps.Delete(server.Id, serverWithips.Ips[1].Id, true);
                Config.waitIpRemoved(serverWithips.Ips[1].Id);
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
            }
        }

        #region Firewall policy

        public void GetFirewallPolicyTest()
        {

            var curIP = server.Ips.FirstOrDefault(ip => ip.FirewallPolicy != null && ip.FirewallPolicy.Count > 0);
            var result = client.ServerIps.GetFirewallPolicies(server.Id, curIP.Id);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Count > 0);
        }

        [TestMethod]
        public void UpdateFirewallPolicy()
        {
            int prevCount = 0;
            var newRules = new System.Collections.Generic.List<POCO.Requests.FirewallPolicies.CreateFirewallPocliyRule>();
            newRules.Add(new POCO.Requests.FirewallPolicies.CreateFirewallPocliyRule()
            {
                PortTo = 80,
                PortFrom = 80,
                Protocol = RuleProtocol.TCP,
                Source = "0.0.0.0"

            });
            firewallPolicy = client.FirewallPolicies.Create(new POCO.Requests.FirewallPolicies.CreateFirewallPolicyRequest
            {
                Description = ".netTestFirewall" + random.Next(10, 30),
                Name = ".netFW" + random.Next(10, 30),
                Rules = newRules
            });
            Config.waitFirewallPolicyReady(firewallPolicy.Id);
            var curIP = server.Ips.FirstOrDefault();
            foreach (var ip in server.Ips)
            {
                //check the policy does not exist
                if (ip.FirewallPolicy != null && ip.FirewallPolicy.Any(po => po.Id == firewallPolicy.Id))
                    continue;
                else
                {
                    curIP = ip;
                    break;
                }
            }
            if (curIP.FirewallPolicy != null)
                prevCount = curIP.FirewallPolicy.Count;
            var result = client.ServerIps.UpdateFirewallPolicy(server.Id, curIP.Id, firewallPolicy.Id);
            Assert.IsNotNull(result);
            Config.waitServerReady(server.Id);
            //give the server time to update
            var resultserver = client.Servers.Show(result.Id);
            var resultIP = result.Ips.FirstOrDefault(ip => ip.Id == curIP.Id);
            Assert.IsTrue(resultIP.FirewallPolicy.Any(fp => fp.Id == firewallPolicy.Id));

            Config.waitServerReady(server.Id);

            GetFirewallPolicyTest();
        }

        #endregion

        #region loadbalancers
        [TestMethod]
        public void CreateLoadBalancer()
        {
            Random random = new Random();
            loadBalancer = client.LoadBalancer.Create(new POCO.Requests.LoadBalancer.CreateLoadBalancerRequest
            {
                Name = "LBTest",
                Description = "LBdesc",
                HealthCheckInterval = 1,
                Persistence = true,
                PersistenceTime = 30,
                HealthCheckTest = HealthCheckTestTypes.TCP,
                Method = LoadBalancerMethod.ROUND_ROBIN,
                Rules = new System.Collections.Generic.List<POCO.Requests.LoadBalancer.LoadBalancerRuleRequest>()
                    {
                        {new POCO.Requests.LoadBalancer.LoadBalancerRuleRequest()
                        {
                            PortBalancer=80,
                            Protocol=LBRuleProtocol.TCP,
                            Source="0.0.0.0",
                            PortServer=80
                        }
                        }
                    }
            });
            Config.waitLoadBalancerReady(loadBalancer.Id);
            var serverWithIps = client.Servers.Show(server.Id);
            var currentIp = serverWithIps.Ips[random.Next(0, serverWithIps.Ips.Count - 1)];

            var result = client.ServerIps.CreateLoadBalancer(serverWithIps.Id, currentIp.Id, loadBalancer.Id);
            Config.waitServerReady(server.Id);
            var updatedLoadBalancer = client.LoadBalancer.GetLoadBalancerServerIps(loadBalancer.Id);

            Assert.IsNotNull(result);
            //check if loadbalancer does have the server IP
            Assert.IsTrue(updatedLoadBalancer.Any(ip => ip.Id == currentIp.Id));
        }

        [TestMethod]
        public void GetLoadBalancer()
        {
            List<OneAndOne.POCO.Response.Servers.LoadBalancers> loadbalancer = null;
            var serverWithIps = client.Servers.Show(server.Id);
            if (serverWithIps.Ips.Any(ip => ip.LoadBalancers != null && ip.LoadBalancers.Count > 0))
            {
                var curIP = serverWithIps.Ips.FirstOrDefault(ip => ip.LoadBalancers != null && ip.LoadBalancers.Count > 0);
                loadbalancer = client.ServerIps.GetLoadBalancer(server.Id, curIP.Id);
                Assert.IsNotNull(loadbalancer);
                Assert.IsNotNull(loadbalancer.Count > 0);
            }
        }

        #endregion
    }
}
