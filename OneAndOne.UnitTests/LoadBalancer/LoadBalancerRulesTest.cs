using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Respones;
using OneAndOne.POCO.Respones.LoadBalancers;

namespace OneAndOne.UnitTests.LoadBalancer
{
    [TestClass]
    public class LoadBalancerRulesTest
    {
        static OneAndOneClient client = new OneAndOneClient();
        [TestMethod]
        public void GetLoadBalancersRules()
        {
            Random random = new Random();
            var loadBalancers = client.LoadBalancers.Get();
            var loadBalancer = loadBalancers[random.Next(loadBalancers.Count - 1)];
            var result = client.LoadBalancers.GetLoadBalancerRules(loadBalancer.Id);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShowLoadBalancerRule()
        {
            var loadBalancers = client.LoadBalancers.Get();
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
                var result = client.LoadBalancers.ShowLoadBalancerRule(loadBalancer.Id, loadBalancer.Rules[0].Id);

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
            }
        }

        [TestMethod]
        public void AddFLoadBalancerRule()
        {
            Random random = new Random();
            var loadBalancers = client.LoadBalancers.Get();
            var loadBalancer = loadBalancers[0];
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
                    Source = "0.0.0.0"
                    }}
                }
            };

            var result = client.LoadBalancers.CreateLoadBalancerRule(request, loadBalancer.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void RemoveLoadBalancerRule()
        {
            Random random = new Random();
            var loadBalancers = client.LoadBalancers.Get();
            OneAndOne.POCO.Respones.LoadBalancers.LoadBalancerResponse loadBalancer = null;
            foreach (var item in loadBalancers)
            {

                if (item.Rules != null && item.Rules.Count > 0)
                {
                    loadBalancer = item;
                    break;
                }
            }
            if (loadBalancer != null && loadBalancer.Rules != null && loadBalancer.Rules.Count > 0)
            {
                var result = client.LoadBalancers.DeleteLoadBalancerRules(loadBalancer.Id, loadBalancer.Rules[0].Id);

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
            }
        }
    }
}
