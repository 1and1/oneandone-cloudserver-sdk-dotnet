using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Response;

namespace OneAndOne.UnitTests.FirewallPolicies
{
    [TestClass]
    public class FirewallpoliciesTest
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

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check if the policy exists
            var policyresult = client.FirewallPolicies.Show(result.Id);
            Assert.IsNotNull(policyresult);
            Assert.IsNotNull(result.Id);
            Config.waitFirewallPolicyReady(result.Id);
            firewall = result;
        }

        [ClassCleanup]
        static public void TestClean()
        {
            if (firewall != null)
            {
                Config.waitFirewallPolicyReady(firewall.Id);
                DeleteFirewallPolicy();
            }
        }

        [TestMethod]
        public void GetFirewallpolicies()
        {
            var result = client.FirewallPolicies.Get();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void ShowFirewallPolicy()
        {
            var result = client.FirewallPolicies.Show(firewall.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void UpdateFirewallPolicy()
        {
            var result = client.FirewallPolicies.Update(new POCO.Requests.FirewallPolicies.UpdateFirewallPolicyRequest()
            {
                Name = "Updated" + firewall.Name,
                Description = "UpdDesc" + firewall.Description
            }, firewall.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check if the policy is Updated
            var policyresult = client.FirewallPolicies.Show(result.Id);
            Assert.IsNotNull(policyresult);
            Assert.IsNotNull(result.Id);
            Assert.AreEqual(result.Name, policyresult.Name);
            Assert.AreEqual(result.Description, policyresult.Description);
        }

        static public void DeleteFirewallPolicy()
        {
            var result = client.FirewallPolicies.Delete(firewall.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check if the policy is removed
            var policyresult = client.FirewallPolicies.Show(result.Id);
            Assert.IsTrue(policyresult.State == "REMOVING");
        }
    }
}
