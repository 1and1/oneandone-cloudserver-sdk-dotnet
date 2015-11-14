using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Respones.LoadBalancers;

namespace OneAndOne.UnitTests.LoadBalancer
{
    [TestClass]
    public class LoadBalancerTest
    {
        static OneAndOneClient client = new OneAndOneClient();
        [TestMethod]
        public void GetLoadBalancers()
        {
            var result = client.LoadBalancers.Get();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void ShowLoadBalancer()
        {
            Random random = new Random();

            var loadBalancers = client.LoadBalancers.Get();
            var loadBalancer = loadBalancers[random.Next(loadBalancers.Count - 1)];

            var result = client.LoadBalancers.Show(loadBalancer.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void CreateLoadBalancer()
        {
            Random random = new Random();
            var result = client.LoadBalancers.Create(new POCO.Requests.LoadBalancer.CreateLoadBalancerRequest()
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

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void UpdateLoadBalancer()
        {
            Random random = new Random();
            var loadBalancers = client.LoadBalancers.Get().Where(str => str.Name.Contains("LBTest")).ToList();
            var loadBalancer = loadBalancers[random.Next(loadBalancers.Count - 1)];
            var result = client.LoadBalancers.Update(new POCO.Requests.LoadBalancer.UpdateLoadBalancerRequest()
            {
                HealthCheckInterval = 100,
                HealthCheckTest = HealthCheckTestTypes.NONE,
                Method = LoadBalancerMethod.ROUND_ROBIN,
                Persistence = false,
                Name = "UpdatedLB" + loadBalancer.Name

            }, loadBalancer.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void DeleteLoadBalancer()
        {
            Random random = new Random();
            var loadBalancers = client.LoadBalancers.Get().Where(str => str.Name.Contains("LBTest")).ToList();
            var loadBalancer = loadBalancers[random.Next(loadBalancers.Count - 1)];
            var result = client.LoadBalancers.Delete(loadBalancer.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void DeleteAllTestLoadBalancer()
        {
            Random random = new Random();
            var loadBalancers = client.LoadBalancers.Get().Where(str => str.Name.Contains("LBTest")).ToList();
            foreach (var item in loadBalancers)
            {
                var result = client.LoadBalancers.Delete(item.Id);
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
            }
        }
    }
}
