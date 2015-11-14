using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using System.Collections.Generic;

namespace OneAndOne.UnitTests.LoadBalancer
{
    [TestClass]
    public class LoadBalancerServerIpsTest
    {
        static OneAndOneClient client = new OneAndOneClient();
        [TestMethod]
        public void GetLoadBalancerServerIps()
        {
            Random random = new Random();
            var loadBalancers = client.LoadBalancers.Get();
            var loadBalancer = loadBalancers[random.Next(loadBalancers.Count - 1)];
            var result = client.LoadBalancers.GetLoadBalancerServerIps(loadBalancer.Id);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShowLoadBalancerServerIp()
        {
            var loadBalancers = client.LoadBalancers.Get();
            var loadBalancer = loadBalancers[0];
            foreach (var item in loadBalancers)
            {
                if (item.ServerIps != null && item.ServerIps.Count > 0)
                {
                    loadBalancer = item;
                    break;
                }
            }
            if (loadBalancer.ServerIps != null && loadBalancer.ServerIps.Count > 0)
            {
                var result = client.LoadBalancers.ShowLoadBalancerServerIp(loadBalancer.Id, loadBalancer.ServerIps[0].Id);

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
            }
        }

        [TestMethod]
        public void AssignLoadBalancerServerIp()
        {
            Random random = new Random();
            var servers = client.Servers.Get();
            var loadBalancers = client.LoadBalancers.Get();
            var loadBalancer = loadBalancers[0];
            OneAndOne.POCO.Respones.Servers.IpAddress ipAddress = null;
            if (servers.Count > 0)
            {
                foreach (var server in servers)
                {
                    if (server.Ips != null && server.Ips.Count > 0)
                    {
                        ipAddress = server.Ips[0];
                        break;
                    }
                }
                if (ipAddress != null)
                {
                    var iptoAdd = new List<string>();
                    iptoAdd.Add(ipAddress.Id);
                    var result = client.LoadBalancers.CreateLoadBalancerServerIPs(new POCO.Requests.LoadBalancers.AssignLoadBalancerServerIpsRequest()
                        {
                            ServerIps = iptoAdd
                        }, loadBalancer.Id);

                    Assert.IsNotNull(result);
                    Assert.IsNotNull(result.Id);
                }
            }
        }

        [TestMethod]
        public void UnassignLoadBalancerServerIp()
        {
            Random random = new Random();
            var loadBalancers = client.LoadBalancers.Get();
            OneAndOne.POCO.Respones.LoadBalancers.LoadBalancerResponse loadBalancer = null;
            foreach (var item in loadBalancers)
            {
                if (item.ServerIps != null && item.ServerIps.Count > 0)
                {
                    loadBalancer = item;
                    break;
                }
            }
            if (loadBalancer != null && loadBalancer.ServerIps != null && loadBalancer.ServerIps.Count > 0)
            {
                var result = client.LoadBalancers.DeleteLoadBalancerServerIP(loadBalancer.Id, loadBalancer.ServerIps[0].Id);

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
            }
        }
    }
}
