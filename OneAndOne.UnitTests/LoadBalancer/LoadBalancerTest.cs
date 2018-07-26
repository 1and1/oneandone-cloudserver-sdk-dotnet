using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Response.LoadBalancers;

namespace OneAndOne.UnitTests.LoadBalancer
{
    [TestClass]
    public class LoadBalancerTest
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

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            var createdLoadBalancer = client.LoadBalancer.Show(result.Id);
            Assert.IsNotNull(createdLoadBalancer.Id);
            loadBalancer = createdLoadBalancer;
        }

        [ClassCleanup]
        static public void TestClean()
        {
            if (loadBalancer != null)
            {
                Config.waitLoadBalancerReady(loadBalancer.Id);
                DeleteLoadBalancer();
            }
        }


        [TestMethod]
        public void GetLoadBalancers()
        {
            var result = client.LoadBalancer.Get();

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void ShowLoadBalancer()
        {
            var result = client.LoadBalancer.Show(loadBalancer.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void UpdateLoadBalancer()
        {
            var result = client.LoadBalancer.Update(new POCO.Requests.LoadBalancer.UpdateLoadBalancerRequest()
            {
                HealthCheckInterval = 100,
                HealthCheckTest = HealthCheckTestTypes.TCP,
                Method = LoadBalancerMethod.ROUND_ROBIN,
                Persistence = false,
                Name = "UpdatedLB" + loadBalancer.Name,
                HealthCheckPathParse = "regex",
                HealthCheckPath = "google.com"


            }, loadBalancer.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            Config.waitLoadBalancerReady(loadBalancer.Id);
            var updatedLoadBalancer = client.LoadBalancer.Show(result.Id);
            Assert.IsNotNull(updatedLoadBalancer.Id);
            Assert.AreEqual(updatedLoadBalancer.Name, result.Name);
            //Assert.IsFalse(updatedLoadBalancer.Persistence);
            Assert.AreEqual(updatedLoadBalancer.HealthCheckInterval, 100);
            Assert.AreEqual(updatedLoadBalancer.HealthCheckTest, HealthCheckTestTypes.TCP);
            Assert.AreEqual(updatedLoadBalancer.Method, LoadBalancerMethod.ROUND_ROBIN);
        }

        static public void DeleteLoadBalancer()
        {
            Random random = new Random();
            var result = client.LoadBalancer.Delete(loadBalancer.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //check if the load balancer is removed
            var removedLoadBalancer = client.LoadBalancer.Show(result.Id);
            Assert.IsTrue(removedLoadBalancer.State == "REMOVING");
        }
    }
}
