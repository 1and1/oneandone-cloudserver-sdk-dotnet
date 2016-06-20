using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Requests.Servers;
using OneAndOne.POCO.Response;
using OneAndOne.POCO.Response.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OneAndOne.UnitTests
{
    [TestClass]
    public class ServerIpsTest
    {
        static OneAndOneClient client = OneAndOneClient.Instance();
        Random random = new Random();

        [TestMethod]
        public void GetServerIPList()
        {
            var servers = client.Servers.Get();
            var server = servers[random.Next(servers.Count - 1)];
            if (server.Status.State == ServerState.DEPLOYING || server.Status.State == ServerState.REMOVING)
            {
                return;
            }
            var result = client.ServerIps.Get(server.Id);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void AddServerIP()
        {
            var servers = client.Servers.Get(null, null, null, "ServerTest");
            var server = servers[random.Next(servers.Count - 1)];
            int previousIpCount = 0;
            foreach (var item in servers)
            {
                if (item.Status.State == ServerState.DEPLOYING || item.Status.State == ServerState.REMOVING || item.Ips.Count >= 5)
                {
                    return;
                }
                else
                {
                    server = item;
                    previousIpCount = item.Ips.Count;
                    break;
                }
            }

            var result = client.ServerIps.Create(new POCO.Requests.Servers.CreateServerIPRequest()
                {
                    Type = IPType.IPV4
                }, server.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //give the server time to update
            var resultserver = client.Servers.Show(result.Id);
            while (resultserver.Ips.Count == previousIpCount)
            {
                Thread.Sleep(2000);
                resultserver = client.Servers.Show(result.Id);
            }
            Assert.IsNotNull(resultserver.Ips.Count > previousIpCount);
        }

        [TestMethod]
        public void ShowIP()
        {
            var servers = client.Servers.Get();
            var server = servers[random.Next(servers.Count - 1)];
            if (server.Status.State == ServerState.DEPLOYING || server.Status.State == ServerState.REMOVING)
            {
                return;
            }
            var result = client.ServerIps.Show(server.Id, server.Ips[0].Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        [TestMethod]
        public void DeleteIP()
        {
            var servers = client.Servers.Get(null, null, null, "ServerTest");
            var server = servers[random.Next(servers.Count - 1)];
            int previousIpCount = 0;

            foreach (var item in servers)
            {
                if (item.Ips.Count > 1)
                {
                    server = item;
                    previousIpCount = item.Ips.Count;
                    break;
                }
            }
            if (server.Status.State == ServerState.DEPLOYING || server.Status.State == ServerState.REMOVING || server.Ips.Count == 1)
            {
                return;
            }
            var result = client.ServerIps.Delete(server.Id, server.Ips[new Random().Next(0, server.Ips.Count - 1)].Id, true);
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
            //give the server time to update
            var resultserver = client.Servers.Show(result.Id);
            while (resultserver.Ips.Count == previousIpCount)
            {
                Thread.Sleep(2000);
                resultserver = client.Servers.Show(result.Id);
            }
            Assert.IsNotNull(resultserver.Ips.Count < previousIpCount);
        }

        #region Firewall policy

        [TestMethod]
        public void GetFirewallPolicyTest()
        {

            var servers = client.Servers.Get();
            List<OneAndOne.POCO.Response.Servers.FirewallPolicyResponse> result = null;
            foreach (var item in servers)
            {
                Thread.Sleep(1000);
                var server = client.Servers.Show(item.Id);
                if (server.Ips.Any(ip => ip.FirewallPolicy != null && ip.FirewallPolicy.Count > 0))
                {
                    var curIP = server.Ips.FirstOrDefault(ip => ip.FirewallPolicy != null && ip.FirewallPolicy.Count > 0);
                    result = client.ServerIps.GetFirewallPolicies(item.Id, curIP.Id);
                    Assert.IsNotNull(result);
                    Assert.IsNotNull(result.Count > 0);
                    break;

                }
            }
        }

        [TestMethod]
        public void DeleteFirewallPolicy()
        {
            var servers = client.Servers.Get();
            foreach (var item in servers)
            {
                Thread.Sleep(1000);
                var server = client.Servers.Show(item.Id);
                if (server.Ips.Any(ip => ip.FirewallPolicy != null && ip.FirewallPolicy.Count > 0))
                {
                    var curIP = server.Ips.FirstOrDefault(ip => ip.FirewallPolicy != null && ip.FirewallPolicy.Count > 0);

                    var result = client.ServerIps.DeleteFirewallPolicy(item.Id, curIP.Id);
                    Assert.IsNotNull(result);
                    break;
                }
            }
        }

        [TestMethod]
        public void UpdateFirewallPolicy()
        {
            var random = new Random();
            int prevCount = 0;
            var servers = client.Servers.Get().Where(ser => ser.Name.Contains("ServerTest") || ser.Name.Contains("Updated")).ToList(); ;
            var firewallPolicies = client.FirewallPolicies.Get();
            foreach (var item in servers)
            {
                Thread.Sleep(1000);
                var server = client.Servers.Show(item.Id);
                var policyToAdd = firewallPolicies[random.Next(0, firewallPolicies.Count - 1)].Id;
                var curIP = server.Ips.FirstOrDefault();
                foreach (var ip in server.Ips)
                {
                    //check the policy does not exist
                    if (ip.FirewallPolicy != null && ip.FirewallPolicy.Any(po => po.Id == policyToAdd))
                        continue;
                    else
                    {
                        curIP = ip;
                        break;
                    }
                }
                //check the policy does not exist
                if (curIP.FirewallPolicy != null && curIP.FirewallPolicy.Any(po => po.Id == policyToAdd))
                    continue;
                if (curIP.FirewallPolicy != null)
                    prevCount = curIP.FirewallPolicy.Count;
                var result = client.ServerIps.UpdateFirewallPolicy(item.Id, curIP.Id, policyToAdd);
                Assert.IsNotNull(result);
                //give the server time to update
                var resultserver = client.Servers.Show(result.Id);
                var resultIP = result.Ips.FirstOrDefault(ip => ip.Id == curIP.Id);
                Assert.IsTrue(resultIP.FirewallPolicy.Any(fp => fp.Id == policyToAdd));
                break;
            }
        }

        #endregion

        #region loadbalancers
        [TestMethod]
        public void CreateLoadBalancer()
        {
            Random random = new Random();
            var servers = client.Servers.Get();
            var loadBalancer = client.LoadBalancer.Get();
            foreach (var item in servers)
            {
                var currentIp = item.Ips[random.Next(0, item.Ips.Count - 1)];
                var currentloadBalancer = loadBalancer[random.Next(0, loadBalancer.Count - 1)];
                var result = client.ServerIps.CreateLoadBalancer(item.Id, currentIp.Id, currentloadBalancer.Id);
                var updatedLoadBalancer = client.LoadBalancer.GetLoadBalancerServerIps(currentloadBalancer.Id);
                Assert.IsNotNull(result);
                //check if loadbalancer does have the server IP
                Assert.IsTrue(updatedLoadBalancer.Any(ip => ip.Id == currentIp.Id));
                break;
            }
        }

        [TestMethod]
        public void GetLoadBalancer()
        {
            var servers = client.Servers.Get();
            List<OneAndOne.POCO.Response.Servers.LoadBalancers> loadbalancer = null;
            foreach (var item in servers)
            {
                Thread.Sleep(1000);
                var server = client.Servers.Show(item.Id);
                if (server.Ips.Any(ip => ip.LoadBalancers != null && ip.LoadBalancers.Count > 0))
                {
                    var curIP = server.Ips.FirstOrDefault(ip => ip.LoadBalancers != null && ip.LoadBalancers.Count > 0);
                    loadbalancer = client.ServerIps.GetLoadBalancer(item.Id, curIP.Id);
                    Assert.IsNotNull(loadbalancer);
                    Assert.IsNotNull(loadbalancer.Count > 0);
                    break;
                }
            }


        }

        [TestMethod]
        public void DeleteLoadBalancer()
        {
            var servers = client.Servers.Get();
            foreach (var item in servers)
            {
                var server = client.Servers.Show(item.Id);
                Thread.Sleep(1000);
                if (server.Ips.Any(ip => ip.LoadBalancers != null && ip.LoadBalancers.Count > 0))
                {
                    var curIP = server.Ips.FirstOrDefault(ip => ip.LoadBalancers != null && ip.LoadBalancers.Count > 0);
                    var result = client.ServerIps.DeleteLoadBalancer(item.Id, curIP.Id, curIP.LoadBalancers[0].Id);
                    Assert.IsNotNull(result);
                    var updatedLoadBalancer = client.LoadBalancer.GetLoadBalancerServerIps(curIP.LoadBalancers[0].Id);
                    Assert.IsNotNull(result);
                    //check if loadbalancer does notk have the server IP
                    Assert.IsTrue(!updatedLoadBalancer.Any(ip => ip.Id == curIP.Id));
                    break;
                }
            }

        }

        #endregion
    }
}
