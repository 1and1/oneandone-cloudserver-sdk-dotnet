using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using System.Collections.Generic;
using OneAndOne.POCO.Response;
using OneAndOne.POCO.Response.Servers;

namespace OneAndOne.UnitTests.FirewallPolicies
{
    [TestClass]
    public class FirewallpolicyServerIpsTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance(Config.Configuration);
        static public POCO.Response.FirewallPolicyResponse firewall = null;
        static ServerResponse server = null;

        [ClassInitialize]
        static public void TestInit(TestContext context)
        {
            Random random = new Random();
            server = Config.CreateTestServer("firewall servers test .net");

            Config.waitServerReady(server.Id);
            server = client.Servers.Show(server.Id);
            //create fw policy
            var newRules = new System.Collections.Generic.List<POCO.Requests.FirewallPolicies.CreateFirewallPocliyRule>();
            newRules.Add(new POCO.Requests.FirewallPolicies.CreateFirewallPocliyRule()
            {
                PortTo = 80,
                PortFrom = 80,
                Protocol = RuleProtocol.TCP,
                Source = "0.0.0.0"

            });
            var result = client.FirewallPolicies.Create(new POCO.Requests.FirewallPolicies.CreateFirewallPolicyRequest()
            {
                Description = ".netTestFirewall" + random.Next(10, 30),
                Name = ".netFW" + random.Next(10, 30),
                Rules = newRules
            });

            Config.waitFirewallPolicyReady(result.Id);
            firewall = result;
            //add server ip to a firewall policy
            var iptoAdd = new List<string>();
            iptoAdd.Add(server.Ips[0].Id);
            var serverIp = client.FirewallPolicies.CreateFirewallPolicyServerIPs(new POCO.Requests.FirewallPolicies.AssignFirewallServerIPRequest() { ServerIps = iptoAdd }, firewall.Id);
            Assert.IsNotNull(serverIp);
            Assert.IsNotNull(serverIp.Id);
            var updatedpolicy = client.FirewallPolicies.Show(serverIp.Id);
            Assert.IsTrue(updatedpolicy.ServerIps.Any(ip => ip.Id == server.Ips[0].Id));
        }

        [ClassCleanup]
        static public void TestClean()
        {
            if (firewall != null)
            {
                UnassignFirewallPolicyServerIp();
                Config.waitFirewallPolicyReady(firewall.Id);
                client.FirewallPolicies.Delete(firewall.Id);
            }
            Config.waitServerReady(server.Id);
            client.Servers.Delete(server.Id, false);
        }
        [TestMethod]
        public void GetFirewallPolicyServerIps()
        {
            var result = client.FirewallPolicies.GetFirewallPolicyServerIps(firewall.Id);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShowFirewallPolicyServerIp()
        {
            var fw = client.FirewallPolicies.Show(firewall.Id);
            var result = client.FirewallPolicies.ShowFirewallPolicyServerIp(firewall.Id, fw.ServerIps[0].Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        static public void UnassignFirewallPolicyServerIp()
        {
            var result = client.FirewallPolicies.DeleteFirewallPolicyServerIP(firewall.Id, server.Ips[0].Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            Config.waitFirewallPolicyReady(firewall.Id);
            var updatedpolicy = client.FirewallPolicies.Show(firewall.Id);
            Assert.IsTrue(!updatedpolicy.ServerIps.Any(ip => ip.Id == firewall.ServerIps[0].Id));
        }
    }
}
