using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAndOne.Client;
using OneAndOne.POCO.Requests.Servers;
using OneAndOne.POCO.Respones;
using OneAndOne.POCO.Respones.Servers;
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
        static OneAndOneClient client = new OneAndOneClient();
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
            var servers = client.Servers.Get();
            var server = servers[random.Next(servers.Count - 1)];
            if (server.Status.State == ServerState.DEPLOYING || server.Status.State == ServerState.REMOVING)
            {
                return;
            }
            var result = client.ServerIps.Create(new POCO.Requests.Servers.CreateServerIPRequest()
                {
                    Type = IPType.IPV4
                }, server.Id);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
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
            var servers = client.Servers.Get();
            var server = servers[random.Next(servers.Count - 1)];
            while (server.Ips.Count == 1)
            {
                server = servers[random.Next(servers.Count - 1)];
            }
            if (server.Status.State == ServerState.DEPLOYING || server.Status.State == ServerState.REMOVING || server.Ips.Count == 1)
            {
                return;
            }
            var result = client.ServerIps.Delete(server.Id, server.Ips[0].Id, false);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Id);
        }

        #region Firewall policy

        [TestMethod]
        public void GetFirewallPolicyTest()
        {

            var servers = client.Servers.Get();
            List<OneAndOne.POCO.Respones.Servers.FirewallPolicyResponse> result = null;
            foreach (var item in servers)
            {
                Thread.Sleep(1000);
                var server = client.Servers.Show(item.Id);
                if (server.Ips.Any(ip => ip.FirewallPolicy != null && ip.FirewallPolicy.Count > 0))
                {
                    var curIP = server.Ips.FirstOrDefault(ip => ip.FirewallPolicy != null && ip.FirewallPolicy.Count > 0);
                    result = client.ServerIps.GetFirewallPolicies(item.Id, item.Ips[0].Id);
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
            var servers = client.Servers.Get();
            foreach (var item in servers)
            {
                Thread.Sleep(1000);
                var server = client.Servers.Show(item.Id);
                if (server.Ips.Any(ip => ip.FirewallPolicy != null && ip.FirewallPolicy.Count > 0))
                {
                    var curIP = server.Ips.FirstOrDefault(ip => ip.FirewallPolicy != null && ip.FirewallPolicy.Count > 0);
                    {
                        Thread.Sleep(1000);
                        var result = client.ServerIps.UpdateFirewallPolicy(item.Id, curIP.Id, curIP.FirewallPolicy[0].Id);
                        Assert.IsNotNull(result);
                    }
                    break;
                }
            }
        }

        #endregion

        #region loadbalancers
        [TestMethod]
        public void CreateLoadBalancer()
        {
            var servers = client.Servers.Get();
            foreach (var item in servers)
            {
                try
                {
                    var loadBalancer = client.LoadBalancers.Get();
                    var result = client.ServerIps.CreateLoadBalancer(item.Id, item.Ips[0].Id, loadBalancer[0].id);
                    Assert.IsNotNull(result);
                    break;
                }
                catch (Exception ex) { }
                break;
            }
        }

        [TestMethod]
        public void GetLoadBalancer()
        {
            var servers = client.Servers.Get();
            List<OneAndOne.POCO.Respones.Servers.LoadBalancers> loadbalancer = null;
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
                }
            }

        }

        #endregion
    }
}
