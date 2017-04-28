using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Response;

namespace OneAndOne.UnitTests.FirewallPolicies
{
    [TestClass]
    public class FirewallPolicyRulesTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance(Config.Configuration);
        static public FirewallPolicyResponse firewall = null;

        [ClassInitialize]
        static public void TestInit(TestContext context)
        {
            Random random = new Random();
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

            //add rule to the policy
            var ruleSource = "0.0.0.0";
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
            firewall = client.FirewallPolicies.CreateFirewallPolicyRule(request, firewall.Id);

            Assert.IsNotNull(firewall);
            Assert.IsNotNull(firewall.Id);
            //check the policy rule is Added
            var updatedpolicy = client.FirewallPolicies.Show(firewall.Id);
            Assert.IsTrue(updatedpolicy.Rules.Any(ru => ru.PortFrom == randomPort && ru.PortTo == randomPort && ru.Source == ruleSource));
            Config.waitFirewallPolicyReady(result.Id);
        }

        [ClassCleanup]
        static public void TestClean()
        {
            if (firewall != null)
            {
                RemoveFirewallPolicyRule();
                Config.waitFirewallPolicyReady(firewall.Id);
                client.FirewallPolicies.Delete(firewall.Id);
            }
        }

        [TestMethod]
        public void GetShowFirewallPolicyRules()
        {
            var result = client.FirewallPolicies.GetFirewallPolicyRules(firewall.Id);
            Assert.IsNotNull(result);

            var show = client.FirewallPolicies.ShowFirewallPolicyRule(firewall.Id, firewall.Rules[0].Id);

            Assert.IsNotNull(show);
            Assert.IsNotNull(show.Id);
        }


        static public void RemoveFirewallPolicyRule()
        {
            var result = client.FirewallPolicies.DeleteFirewallPolicyRules(firewall.Id, firewall.Rules[0].Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check the policy rule is removed
            var updatedpolicy = client.FirewallPolicies.Show(firewall.Id);
            Assert.IsTrue(!updatedpolicy.Rules.Any(ru => ru.Id == firewall.Rules[0].Id));
        }
    }
}
