using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Respones;

namespace OneAndOne.UnitTests.FirewallPolicies
{
    [TestClass]
    public class FirewallPolicyRulesTest
    {
        static OneAndOneClient client = new OneAndOneClient();
        [TestMethod]
        public void GetFirewallPolicyRules()
        {
            Random random = new Random();
            var firewallpolicies = client.FirewallPolicies.Get();
            var firewallpolicy = firewallpolicies[random.Next(firewallpolicies.Count - 1)];
            var result = client.FirewallPolicies.GetFirewallPolicyRules(firewallpolicy.Id);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShowFirewallPolicyRule()
        {
            var firewallpolicies = client.FirewallPolicies.Get();
            var firewallpolicy = firewallpolicies[0];
            foreach (var item in firewallpolicies)
            {
                if (item.Rules != null && item.Rules.Count > 0)
                {
                    firewallpolicy = item;
                    break;
                }
            }
            if (firewallpolicy.Rules != null && firewallpolicy.Rules.Count > 0)
            {
                var result = client.FirewallPolicies.ShowFirewallPolicyRule(firewallpolicy.Id, firewallpolicy.Rules[0].Id);

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
            }
        }

        [TestMethod]
        public void AddFirewallPolicyRule()
        {
            Random random = new Random();
            var firewallpolicies = client.FirewallPolicies.Get();
            var firewallpolicy = firewallpolicies[0];
            foreach (var firewall in firewallpolicies)
            {
                if (firewall.Default < 1)
                {
                    firewallpolicy = firewall;
                    break;
                }
            }
            if (firewallpolicy.Default < 1)
            {
                var randomPort = random.Next(1111, 9999);
                var request = new POCO.Requests.FirewallPolicies.AddFirewallPolicyRuleRequest()
                {
                    Rules = new System.Collections.Generic.List<POCO.Requests.FirewallPolicies.RuleRequest>()
                {
                    {new OneAndOne.POCO.Requests.FirewallPolicies.RuleRequest()
                    {
                        PortFrom =randomPort,
                    PortTo = randomPort,
                    Protocol = RuleProtocol.TCP,
                    Source = "0.0.0.0"
                    }}
                }
                };

                var result = client.FirewallPolicies.CreateFirewallPolicyRule(request, firewallpolicy.Id);

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
            }
        }

        [TestMethod]
        public void RemoveFirewallPolicyRule()
        {
            Random random = new Random();
            var firewallpolicies = client.FirewallPolicies.Get();
            OneAndOne.POCO.Respones.FirewallPolicyResponse firewallpolicy = null;
            foreach (var item in firewallpolicies)
            {

                if (item.Rules != null && item.Rules.Count > 0 && item.Default < 1)
                {
                    firewallpolicy = item;
                    break;
                }
            }
            if (firewallpolicy != null && firewallpolicy.Rules != null && firewallpolicy.Rules.Count > 0 && firewallpolicy.Default < 1)
            {
                var result = client.FirewallPolicies.DeleteFirewallPolicyRules(firewallpolicy.Id, firewallpolicy.Rules[0].Id);

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
            }
        }
    }
}
