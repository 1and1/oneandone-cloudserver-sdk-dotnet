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
        static OneAndOneClient client = OneAndOneClient.Instance(Config.Configuration);
        static LoadBalancerResponse loadBalancer = null;

        [ClassInitialize]
        static public void TestInit(TestContext context)
        {
            Random random = new Random();
            var result = client.LoadBalancer.Create(new POCO.Requests.LoadBalancer.CreateLoadBalancerRequest()
            {
                Name = "LBTest" + random.Next(100, 999),
                Description = "LBdesc",
                HealthCheckInterval = 1,
                Persistence = true,
                PersistenceTime = 30,
                HealthCheckTest = HealthCheckTestTypes.NONE,
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

            Config.waitLoadBalancerReady(result.Id);
            loadBalancer = result;

            string ruleSource = "0.0.0.0";
            //add new rule
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

            var newRule = client.LoadBalancer.CreateLoadBalancerRule(request, loadBalancer.Id);

            Assert.IsNotNull(newRule);
            Assert.IsNotNull(newRule.Id);
            Config.waitLoadBalancerReady(loadBalancer.Id);
            //check the rule is Added
            var updatedLoadBalancer = client.LoadBalancer.Show(loadBalancer.Id);
            Assert.IsTrue(updatedLoadBalancer.Rules.Any(ru => ru.PortServer == randomPort && ru.PortBalancer == randomPort && ru.Source == ruleSource));

        }

        [ClassCleanup]
        static public void TestClean()
        {
            if (loadBalancer != null)
            {
                RemoveLoadBalancerRule();
                Config.waitLoadBalancerReady(loadBalancer.Id);
                client.LoadBalancer.Delete(loadBalancer.Id);
            }
        }
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
            var lb = client.LoadBalancer.Show(loadBalancer.Id);
            var result = client.LoadBalancer.ShowLoadBalancerRule(lb.Id, lb.Rules[0].Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        static public void RemoveLoadBalancerRule()
        {
            var lb = client.LoadBalancer.Show(loadBalancer.Id);
            var result = client.LoadBalancer.DeleteLoadBalancerRules(lb.Id, lb.Rules[0].Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check the policy is removed
            var updatedpolicy = client.LoadBalancer.Show(lb.Id);
            Assert.IsFalse(updatedpolicy.Rules.Any(ru => ru.Id == lb.Rules[0].Id));
        }
    }
}
