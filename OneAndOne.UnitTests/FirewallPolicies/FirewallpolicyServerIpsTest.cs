using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using System.Collections.Generic;

namespace OneAndOne.UnitTests.FirewallPolicies
{
    [TestClass]
    public class FirewallpolicyServerIpsTest
    {
        static OneAndOneClient client = new OneAndOneClient();
        [TestMethod]
        public void GetFirewallPolicyServerIps()
        {
            Random random = new Random();
            var firewallpolicies = client.FirewallPolicies.Get();
            var firewallpolicy = firewallpolicies[random.Next(firewallpolicies.Count - 1)];
            var result = client.FirewallPolicies.GetFirewallPolicyServerIps(firewallpolicy.Id);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShowFirewallPolicyServerIp()
        {
            var firewallpolicies = client.FirewallPolicies.Get();
            var firewallpolicy = firewallpolicies[0];
            foreach (var item in firewallpolicies)
            {
                if (item.ServerIps != null && item.ServerIps.Count > 0)
                {
                    firewallpolicy = item;
                    break;
                }
            }
            if (firewallpolicy.ServerIps != null && firewallpolicy.ServerIps.Count > 0)
            {
                var result = client.FirewallPolicies.ShowFirewallPolicyServerIp(firewallpolicy.Id, firewallpolicy.ServerIps[0].Id);

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
            }
        }

        [TestMethod]
        public void AssignFirewallPolicyServerIp()
        {
            Random random = new Random();
            var servers = client.Servers.Get();
            var firewallpolicies = client.FirewallPolicies.Get();
            var firewallpolicy = firewallpolicies[0];
            OneAndOne.POCO.Respones.Servers.IpAddress ipAddress = null;
            if (servers.Count > 0)
            {
                foreach (var server in servers)
                {
                    if (server.Ips != null && server.Ips.Count > 0)
                    {
                        ipAddress = server.Ips[0];
                    }
                }
                if (ipAddress != null)
                {
                    var iptoAdd = new List<string>();
                    iptoAdd.Add(ipAddress.Id);
                    var result = client.FirewallPolicies.CreateFirewallPolicyServerIPs(new POCO.Requests.FirewallPolicies.AssignFirewallServerIPRequest() { ServerIps = iptoAdd }, firewallpolicy.Id);

                    Assert.IsNotNull(result);
                    Assert.IsNotNull(result.Id);
                }
            }
        }

        [TestMethod]
        public void UnassignFirewallPolicyServerIp()
        {
            Random random = new Random();
            var firewallpolicies = client.FirewallPolicies.Get();
            OneAndOne.POCO.Respones.FirewallPolicyResponse firewallpolicy = null;
            foreach (var item in firewallpolicies)
            {
                if (item.ServerIps != null && item.ServerIps.Count > 0)
                {
                    firewallpolicy = item;
                    break;
                }
            }
            if (firewallpolicy != null && firewallpolicy.ServerIps != null && firewallpolicy.ServerIps.Count > 0)
            {
                var result = client.FirewallPolicies.DeleteFirewallPolicyServerIP(firewallpolicy.Id, firewallpolicy.ServerIps[0].Id);

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
            }
        }
    }
}
