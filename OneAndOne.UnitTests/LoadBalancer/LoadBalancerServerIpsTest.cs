using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using System.Collections.Generic;

namespace OneAndOne.UnitTests.LoadBalancer
{
    [TestClass]
    public class LoadBalancerServerIpsTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance();
        [TestMethod]
        public void GetLoadBalancerServerIps()
        {
            Random random = new Random();
            var loadBalancers = client.LoadBalancer.Get();
            var loadBalancer = loadBalancers[random.Next(loadBalancers.Count - 1)];
            var result = client.LoadBalancer.GetLoadBalancerServerIps(loadBalancer.Id);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ShowLoadBalancerServerIp()
        {
            var loadBalancers = client.LoadBalancer.Get();
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
                var result = client.LoadBalancer.ShowLoadBalancerServerIp(loadBalancer.Id, loadBalancer.ServerIps[0].Id);

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
            }
        }

        [TestMethod]
        public void AssignLoadBalancerServerIp()
        {
            Random random = new Random();
            var servers = client.Servers.Get();
            var loadBalancers = client.LoadBalancer.Get();
            var loadBalancer = loadBalancers[random.Next(0, loadBalancers.Count - 1)];
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
                    var result = client.LoadBalancer.CreateLoadBalancerServerIPs(new POCO.Requests.LoadBalancer.AssignLoadBalancerServerIpsRequest()
                        {
                            ServerIps = iptoAdd
                        }, loadBalancer.Id);

                    Assert.IsNotNull(result);
                    Assert.IsNotNull(result.Id);
                    var updatedLoadBalancer = client.LoadBalancer.Show(loadBalancer.Id);
                    //check the ip is added
                    Assert.IsNotNull(updatedLoadBalancer);
                    Assert.IsTrue(updatedLoadBalancer.ServerIps.Any(ip => ip.Id == ipAddress.Id));
                }
            }
        }

        [TestMethod]
        public void UnassignLoadBalancerServerIp()
        {
            Random random = new Random();
            var loadBalancers = client.LoadBalancer.Get();
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
                var result = client.LoadBalancer.DeleteLoadBalancerServerIP(loadBalancer.Id, loadBalancer.ServerIps[0].Id);

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.Id);
                var updatedLoadBalancer = client.LoadBalancer.Show(loadBalancer.Id);
                //check the ip is removed
                Assert.IsNotNull(updatedLoadBalancer);
                Assert.IsFalse(updatedLoadBalancer.ServerIps.Any(ip => ip.Id == loadBalancer.ServerIps[0].Id));
            }
        }
    }
}
