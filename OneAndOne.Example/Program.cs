using OneAndOne.Client;
using OneAndOne.POCO;
using OneAndOne.POCO.Requests.Servers;
using OneAndOne.POCO.Respones;
using OneAndOne.POCO.Respones.LoadBalancers;
using OneAndOne.POCO.Respones.ServerAppliances;
using OneAndOne.POCO.Respones.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OneAndOne.Example
{
    public class Program
    {
        static OneAndOneClient client = OneAndOneClient.Instance();
        static void Main(string[] args)
        {
            GetServers();
            CreateServers();

        }

        static void GetServers()
        {
            //get a list of servers list then in two pages and show three at max per page
            var serversResult = client.Servers.Get(2, 3);
        }

        static void CreateServers()
        {
            string firewallPolicyName = "test firewall policy .net";
            string loadBalancerName = "Test loadBalancer .net";
            string serverName = "Example Server .net";

            //create a firewall policy
            //define the required rules
            Console.WriteLine("Creating Firewall Policy with name " + firewallPolicyName);
            var newRules = new System.Collections.Generic.List<POCO.Requests.FirewallPolicies.CreateFirewallPocliyRule>();
            newRules.Add(new POCO.Requests.FirewallPolicies.CreateFirewallPocliyRule()
            {
                PortTo = 80,
                PortFrom = 80,
                Protocol = RuleProtocol.TCP,
                Source = "0.0.0.0"

            });
            newRules.Add(new POCO.Requests.FirewallPolicies.CreateFirewallPocliyRule()
            {
                PortTo = 443,
                PortFrom = 443,
                Protocol = RuleProtocol.TCP,
                Source = "0.0.0.0"

            });
            newRules.Add(new POCO.Requests.FirewallPolicies.CreateFirewallPocliyRule()
            {
                PortTo = 8447,
                PortFrom = 8447,
                Protocol = RuleProtocol.TCP,
                Source = "0.0.0.0"

            });
            newRules.Add(new POCO.Requests.FirewallPolicies.CreateFirewallPocliyRule()
            {
                PortTo = 3389,
                PortFrom = 3389,

                Protocol = RuleProtocol.TCP,
                Source = "0.0.0.0"

            });
            newRules.Add(new POCO.Requests.FirewallPolicies.CreateFirewallPocliyRule()
            {
                PortTo = 8443,
                PortFrom = 8443,
                Protocol = RuleProtocol.TCP,
                Source = "0.0.0.0"

            });
            var firewallPolicyResult = client.FirewallPolicies.Create(new POCO.Requests.FirewallPolicies.CreateFirewallPolicyRequest()
            {
                Description = "test firewall policy with 80,443,8447,3389 and 8443 ports open",
                Name = firewallPolicyName,
                Rules = newRules
            });

            Console.WriteLine("Creating LoadBalancer with name " + loadBalancerName);
            //create a loadbalancer
            var loadbalancerResult = client.LoadBalancer.Create(new POCO.Requests.LoadBalancer.CreateLoadBalancerRequest()
            {
                Name = loadBalancerName,
                Description = "LB with a round robin method and works on port 80",
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
            //create a public IP and use it for the server creation
            var publicIp = client.PublicIPs.Create(new POCO.Requests.PublicIPs.CreatePublicIPRequest()
            {
                Type = IPType.IPV4,
            });

            Console.WriteLine("Creating Server with name 'Example Server .net'");
            //define the number of cores to give the server
            int vcore = 4;
            //number of cores per processor
            int CoresPerProcessor = 2;
            //get server appliance with OS family type Windows
            var appliances = client.ServerAppliances.Get().Where(app => app.OsFamily == OSFamliyType.Windows);
            ServerAppliancesResponse appliance = null;
            if (appliances == null || appliances.Count() == 0)
            {
                appliance = client.ServerAppliances.Get().FirstOrDefault();
            }
            else
            {
                appliance = appliances.FirstOrDefault();
            }
            //get an availabe public IP and assign to the server
            //var publicIP = client.PublicIPs.Get().FirstOrDefault(ip => ip.State == "ACTIVE" && ip.AssignedTo == null);
            var result = client.Servers.Create(new POCO.Requests.Servers.CreateServerRequest()
            {
                ApplianceId = appliance != null ? appliance.Id : null,
                Name = serverName,
                Description = "a windows server with a windows firewall policy and a loadbalancer",
                Hardware = new POCO.Requests.Servers.HardwareReqeust()
                {
                    CoresPerProcessor = CoresPerProcessor,
                    Hdds = new List<POCO.Requests.Servers.HddRequest>()
                        {
                            {new POCO.Requests.Servers.HddRequest()
                            {
                                IsMain=true,
                                //assign a HDD size that is larger than the recommended min size for the appliance choosen.
                                Size=appliance != null ?appliance.MinHddSize+20:40,
                            }}
                        },
                    Ram = 8,
                    Vcore = vcore
                },
                PowerOn = true,
                Password = "Test123!",
                IpId = publicIp != null ? publicIp.Id : null
            });

            Console.WriteLine("Server created waiting to be deployed and turned on");
            //check if the server is deployed and ready for further operations
            var testServer = client.Servers.Show(result.Id);
            while (testServer.Status.State != ServerState.POWERED_ON)
            {
                Thread.Sleep(1000);
                testServer = client.Servers.Show(testServer.Id);
            }
            Console.WriteLine("Server is Powered up and running");
            //attaching a firewall policy to the server after creation:
            //Get a windows firewall policy by sending the query parameter Windows
            Console.WriteLine(string.Format("Assigning {0} to {1}", firewallPolicyName, serverName));
            var firewallPolicy = client.FirewallPolicies.Get(null, null, null, firewallPolicyName).FirstOrDefault();
            client.ServerIps.UpdateFirewallPolicy(testServer.Id, testServer.Ips[0].Id, firewallPolicy.Id);

            Console.WriteLine(string.Format("Assigning {0} to {1}", loadBalancerName, serverName));
            // attaching a loadbalancer to the server
            var loadBalancer = client.LoadBalancer.Get(null, null, null, loadBalancerName).FirstOrDefault();
            client.ServerIps.CreateLoadBalancer(testServer.Id, testServer.Ips[0].Id, loadBalancer.Id);

            //cleaning up
            Console.WriteLine("Cleaning up all the created test data");
            client.Servers.Delete(testServer.Id, true);
            client.LoadBalancer.Delete(loadBalancer.Id);
            client.FirewallPolicies.Delete(firewallPolicy.Id);
            client.PublicIPs.Delete(publicIp.Id);
            Console.ReadLine();

        }
    }
}
