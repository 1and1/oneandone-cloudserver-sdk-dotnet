using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using System.Collections.Generic;
using OneAndOne.POCO.Response.Servers;
using OneAndOne.POCO.Response.LoadBalancers;

namespace OneAndOne.UnitTests.LoadBalancer
{
    [TestClass]
    public class LoadBalancerServerIpsTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance(Config.Configuration);
        static ServerResponse server = null;
        static LoadBalancerResponse loadBalancer = null;

        [ClassInitialize]
        static public void TestInit(TestContext context)
        {
            Random random = new Random();
            server = Config.CreateTestServer("loadbalancer servers test .net");

            Config.waitServerReady(server.Id);
            server = client.Servers.Show(server.Id);
            //create loadbalancer
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

            loadBalancer = result;
            //add server ip to load balancer
            var iptoAdd = new List<string>();
            iptoAdd.Add(server.Ips[0].Id);
            var lbIps = client.LoadBalancer.CreateLoadBalancerServerIPs(new POCO.Requests.LoadBalancer.AssignLoadBalancerServerIpsRequest()
            {
                ServerIps = iptoAdd
            }, loadBalancer.Id);

            Assert.IsNotNull(lbIps);
            Assert.IsNotNull(lbIps.Id);
            Config.waitLoadBalancerReady(loadBalancer.Id);
            var updatedLoadBalancer = client.LoadBalancer.Show(loadBalancer.Id);
            //check the ip is added
            Assert.IsNotNull(updatedLoadBalancer);
            Assert.IsTrue(updatedLoadBalancer.ServerIps.Any(ip => ip.Id == server.Ips[0].Id));
        }

        [ClassCleanup]
        static public void TestClean()
        {
            if (loadBalancer != null)
            {
                UnassignLoadBalancerServerIp();
                Config.waitLoadBalancerReady(loadBalancer.Id);
                client.LoadBalancer.Delete(loadBalancer.Id);
            }
            Config.waitServerReady(server.Id);
            client.Servers.Delete(server.Id, false);
        }

        [TestMethod]
        public void GetLoadBalancerServerIps()
        {
            var result = client.LoadBalancer.GetLoadBalancerServerIps(loadBalancer.Id);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShowLoadBalancerServerIp()
        {
            var lb = client.LoadBalancer.Show(loadBalancer.Id);
            var result = client.LoadBalancer.ShowLoadBalancerServerIp(lb.Id, lb.ServerIps[0].Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        static public void UnassignLoadBalancerServerIp()
        {
            var lb = client.LoadBalancer.Show(loadBalancer.Id);
            var result = client.LoadBalancer.DeleteLoadBalancerServerIP(lb.Id, lb.ServerIps[0].Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            var updatedLoadBalancer = client.LoadBalancer.Show(lb.Id);
            //check the ip is removed
            Assert.IsNotNull(updatedLoadBalancer);
            Assert.IsFalse(updatedLoadBalancer.ServerIps.Any(ip => ip.Id == lb.ServerIps[0].Id));
        }
    }
}
