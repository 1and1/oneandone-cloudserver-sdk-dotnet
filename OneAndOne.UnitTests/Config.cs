using OneAndOne.Client;
using OneAndOne.Client.RESTHelpers;
using OneAndOne.POCO.Response.Servers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OneAndOne.POCO;

namespace OneAndOne.UnitTests
{
    public class Config
    {
        public static Client.RESTHelpers.Configuration Configuration
        {
            get
            {
                return new Client.RESTHelpers.Configuration
                {
                    ApiKey = ConfigurationManager.AppSettings["APIToken"]

                };
            }
        }

        public static void WaitBlockStorageReady(string blockStorageId)
        {
            var client = OneAndOneClient.Instance(Configuration);
            var blockStorage = client.BlockStorages.Show(blockStorageId);
            while (blockStorage != null && blockStorage.State != "POWERED_ON")
            {
                Thread.Sleep(5000);
                blockStorage = client.BlockStorages.Show(blockStorageId);
            }
        }

        public static void waitServerReady(string ServerId)
        {
            Thread.Sleep(5000);
            var client = OneAndOneClient.Instance(Configuration);
            var server = client.Servers.Show(ServerId);
            while (server != null && ((server.Status.State != POCO.Response.Servers.ServerState.POWERED_ON && server.Status.State != POCO.Response.Servers.ServerState.POWERED_OFF) || (server.Status.Percent != 0 && server.Status.Percent != 99)))
            {
                Thread.Sleep(10000);
                server = client.Servers.Show(ServerId);
            }
        }

        public static void waitServerTurnedOff(string ServerId)
        {
            Thread.Sleep(5000);
            var client = OneAndOneClient.Instance(Configuration);
            var server = client.Servers.Show(ServerId);
            while (server != null && (server.Status.State != POCO.Response.Servers.ServerState.POWERED_OFF || (server.Status.Percent != 0 && server.Status.Percent != 99)))
            {
                Thread.Sleep(10000);
                server = client.Servers.Show(ServerId);
            }
        }

        public static void waitPrivateNetworkReady(string pnId)
        {
            var client = OneAndOneClient.Instance(Configuration);
            var pn = client.PrivateNetworks.Show(pnId);
            while (pn.State != "ACTIVE")
            {
                Thread.Sleep(10000);
                pn = client.PrivateNetworks.Show(pnId);
            }
        }

        public static void waitSharedStorageReady(string shareStorageId)
        {
            var client = OneAndOneClient.Instance(Configuration);
            var sharedStorage = client.SharedStorages.Show(shareStorageId);
            while (sharedStorage.State == "CONFIGURING")
            {
                Thread.Sleep(5000);
                sharedStorage = client.SharedStorages.Show(sharedStorage.Id);
            }
        }

        public static void waitFirewallPolicyReady(string fwId)
        {
            var client = OneAndOneClient.Instance(Configuration);
            var fw = client.FirewallPolicies.Show(fwId);
            while (fw.State == "CONFIGURING")
            {
                Thread.Sleep(5000);
                fw = client.FirewallPolicies.Show(fw.Id);
            }
        }

        public static void waitImageReady(string imgId)
        {
            var client = OneAndOneClient.Instance(Configuration);
            var img = client.Images.Show(imgId);
            while (img.State != "ACTIVE" && img.State!= "ENABLED")
            {
                Thread.Sleep(5000);
                img = client.Images.Show(img.Id);
            }
        }

        public static void waitLoadBalancerReady(string lbId)
        {
            var client = OneAndOneClient.Instance(Configuration);
            var lb = client.LoadBalancer.Show(lbId);
            while (lb.State == "CONFIGURING")
            {
                Thread.Sleep(8000);
                lb = client.LoadBalancer.Show(lb.Id);
            }
        }

        public static void waitMonitoringPolicyReady(string mpId)
        {
            var client = OneAndOneClient.Instance(Configuration);
            var mp = client.MonitoringPolicies.Show(mpId);
            while (mp.State != "ACTIVE")
            {
                Thread.Sleep(5000);
                mp = client.MonitoringPolicies.Show(mp.Id);
            }
        }

        public static void waitIpRemoved(string ipId)
        {

            try
            {
                var client = OneAndOneClient.Instance(Configuration);
                var publicIP = client.PublicIPs.Show(ipId);
                while (publicIP != null && publicIP.State == "REMOVING")
                {
                    Thread.Sleep(5000);
                    publicIP = client.PublicIPs.Show(publicIP.Id);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message != "{\"type\":\"ELEMENT_NOT_FOUND\",\"message\":\"\",\"errors\":null}")
                {
                    throw ex;
                }
            }
        }

        public static ServerResponse CreateTestServer(string serverName, bool powerON=true)
        {
            var client = OneAndOneClient.Instance(Configuration);
            int vcore = 4;
            int CoresPerProcessor = 2;
            var appliances = client.ServerAppliances.Get(null, null, null, "coreos", null);
            POCO.Response.ServerAppliances.ServerAppliancesResponse appliance = null;
            if (appliances == null || !appliances.Any())
            {
                appliance = client.ServerAppliances.Get().FirstOrDefault(a => a.ServerTypeCompatibility.Contains(ServerTypeCompatibility.cloud));
            }
            else
            {
                appliance = appliances.FirstOrDefault(a => a.ServerTypeCompatibility.Contains(ServerTypeCompatibility.cloud));
            }
            var result = client.Servers.Create(new POCO.Requests.Servers.CreateServerRequest()
            {
                ApplianceId = appliance != null ? appliance.Id : null,
                Name = serverName,
                Description = "desc",
                Hardware = new POCO.Requests.Servers.HardwareRequest()
                {
                    CoresPerProcessor = CoresPerProcessor,
                    Hdds = new List<POCO.Requests.Servers.HddRequest>()
                        {
                            {new POCO.Requests.Servers.HddRequest()
                            {
                                IsMain=true,
                                Size=20,
                            }}
                        },
                    Ram = 4,
                    Vcore = vcore
                },
                ServerType = ServerType.cloud,
                PowerOn = powerON,
            });
            return client.Servers.Show(result.Id);
        }
    }


}
