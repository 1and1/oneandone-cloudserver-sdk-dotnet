using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Response;
using OneAndOne.POCO.Response.LoadBalancers;

namespace OneAndOne.UnitTests.LoadBalancer
{
    [TestClass]
    public class LoadBalancerRulesTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance();
        [TestMethod]
        public void GetLoadBalancersRules()
        {
            Random random = new Random();
            var loadBalancers = client.LoadBalancer.Get();
            var loadBalancer = loadBalancers[random.Next(loadBalancers.Count - 1)];
            var result = client.LoadBalancer.GetLoadBalancerRules(loadBalancer.Id);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShowLoadBalancerRule()
        {
            var loadBalancers = client.LoadBalancer.Get();
            var loadBalancer = loadBalancers[0];
            foreach (var item in loadBalancers)
            {
                if (item.Rules != null && item.Rules.Count > 0)
                {
                    loadBalancer = item;
                    break;
                }
            }
            if (loadBalancer.Rules != null && loadBalancer.Rules.Count > 0)
            {
                var result = client.LoadBalancer.ShowLoadBalancerRule(loadBalancer.Id, loadBalancer.Rules[0].Id);

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);

            }
        }

        [TestMethod]
        public void AddFLoadBalancerRule()
        {
            Random random = new Random();
            string ruleSource = "0.0.0.0";
            var loadBalancers = client.LoadBalancer.Get();
            var loadBalancer = loadBalancers[random.Next(0, loadBalancers.Count - 1)];
            var randomPort = random.Next(1111, 9999);
            var request = new POCO.Requests.LoadBalancer.AddLoadBalancerRuleRequest()
            {
                Rules = new System.Collections.Generic.List<POCO.Requests.LoadBalancer.RuleRequest>()
                {
                    {new OneAndOne.POCO.Requests.LoadBalancer.RuleRequest()
                    {
                        PortBalancer =randomPort,
                    PortServer = randomPort,
                    Protocol = LBRuleProtocol.TCP,
                    Source = ruleSource
                    }}
                }
            };

            var result = client.LoadBalancer.CreateLoadBalancerRule(request, loadBalancer.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check the rule is Added
            var updatedLoadBalancer = client.LoadBalancer.Show(loadBalancer.Id);
            Assert.IsTrue(updatedLoadBalancer.Rules.Any(ru => ru.PortServer == randomPort && ru.PortBalancer == randomPort && ru.Source == ruleSource));
        }

        [TestMethod]
        public void RemoveLoadBalancerRule()
        {
            Random random = new Random();
            var loadBalancers = client.LoadBalancer.Get();
            OneAndOne.POCO.Response.LoadBalancers.LoadBalancerResponse loadBalancer = null;
            foreach (var item in loadBalancers)
            {

                if (item.Rules != null && item.Rules.Count > 0)
                {
                    loadBalancer = item;
                    break;
                }
            }
            if (loadBalancer != null && loadBalancer.Rules != null && loadBalancer.Rules.Count > 1)
            {
                var result = client.LoadBalancer.DeleteLoadBalancerRules(loadBalancer.Id, loadBalancer.Rules[0].Id);

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
                //check the policy is Added
                var updatedpolicy = client.LoadBalancer.Show(loadBalancer.Id);
                Assert.IsFalse(updatedpolicy.Rules.Any(ru => ru.Id == loadBalancer.Rules[0].Id));
            }
        }
    }
}
