using Newtonsoft.Json;
using OneAndOne.Client.RESTHelpers;
using OneAndOne.POCO.Requests.Servers;
using OneAndOne.POCO.Respones;
using OneAndOne.POCO.Respones.Servers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.Client.Endpoints.Servers
{
    public class IPs : ResourceBase
    {

        #region Ip operations
        /// <summary>
        /// Returns a list of the server's IPs.
        /// 
        ///  <param name="server_id">Unique server's identifier.</param>
        /// </summary>
        public List<OneAndOne.POCO.Respones.Servers.IpAddress> Get(string server_id)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/ips", Method.GET);
                request.AddUrlSegment("server_id", server_id);

                var result = restclient.Execute<List<OneAndOne.POCO.Respones.Servers.IpAddress>>(request);
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(result.Content);
                }
                return result.Data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Adds a new IP to the server.
        /// </summary>
        /// <param name="server_id">Unique server's identifier.</param>
        /// 
        public ServerResponse Create(CreateServerIPRequest ip, string server_id)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/ips", Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("server_id", server_id);
                request.AddBody(ip);

                var result = restclient.Execute<ServerResponse>(request);
                if (result.StatusCode != HttpStatusCode.Created)
                {
                    throw new Exception(result.Content);
                }
                return result.Data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Returns information about a server's IP.
        /// </summary>
        /// <param name="server_id">Unique server's identifier.</param>
        /// <param name="ip_id">ip_id: required (string ) Unique IP's identifier.</param>
        /// 
        public IpAddress Show(string server_id, string ip_id)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/ips/{ip_id}", Method.GET)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("server_id", server_id);
                request.AddUrlSegment("ip_id", ip_id);

                var result = restclient.Execute<IpAddress>(request);
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(result.Content);
                }
                return result.Data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Releases an IP and optionally removes it
        /// </summary>
        /// <param name="server_id">Unique server's identifier.</param>
        /// <param name="ip_id">ip_id: required (string ) Unique IP's identifier.</param>
        /// <param name="Keep">Set true for releasing the IP without removing it</param>
        /// 
        public ServerResponse Delete(string server_id, string ip_id, bool Keep)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/ips/{ip_id}", Method.DELETE)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddHeader("Content-Type", "application/json");
                request.AddUrlSegment("server_id", server_id);
                request.AddUrlSegment("ip_id", ip_id);
                string keep_ip = Keep.ToString().ToLower();
                request.AddBody(new { keep_ip });

                var result = restclient.Execute<ServerResponse>(request);
                if (result.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new Exception(result.Content);
                }
                return result.Data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region Firewall policy

        /// <summary>
        /// Lists firewall policies assigned to the IP
        /// 
        ///  <param name="server_id">Unique server's identifier.</param>
        ///  <param name="ip_id">Unique server's identifier.</param>
        /// </summary>
        public List<OneAndOne.POCO.Respones.Servers.FirewallPolicyResponse> GetFirewallPolicies(string server_id, string ip_id)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/ips/{ip_id}/firewall_policy", Method.GET);
                request.AddUrlSegment("server_id", server_id);
                request.AddUrlSegment("ip_id", ip_id);

                var result = restclient.Execute<List<OneAndOne.POCO.Respones.Servers.FirewallPolicyResponse>>(request);
                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    throw new Exception(result.Content);
                }
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(result.Content);
                }
                return result.Data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Adds a new firewall policy to the IP
        /// </summary>
        /// <param name="server_id">Unique server's identifier.</param>
        /// <param name="ip_id">ip:  required (string ) Unique IP's identifier.</param>
        /// <param name="policyId"> Unique policy's identifier.</param>
        /// 
        public ServerResponse UpdateFirewallPolicy(string server_id, string ip_id, string policy_id)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/ips/{ip_id}/firewall_policy", Method.PUT)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("server_id", server_id);
                request.AddUrlSegment("ip_id", ip_id);
                string id = policy_id;
                request.AddBody(new { id });

                var result = restclient.Execute<ServerResponse>(request);
                if (result.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new Exception(result.Content);
                }
                return result.Data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Removes firewall policy from the IP
        /// </summary>
        /// <param name="server_id">Unique server's identifier.</param>
        /// <param name="ip_id">ip_id: required (string ) Unique IP's identifier.</param>
        /// 
        public ServerResponse DeleteFirewallPolicy(string server_id, string ip_id)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/ips/{ip_id}/firewall_policy", Method.DELETE)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("server_id", server_id);
                request.AddUrlSegment("ip_id", ip_id);
                request.AddHeader("Content-Type", "application/json");

                var result = restclient.Execute<ServerResponse>(request);
                if (result.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new Exception(result.Content);
                }
                return result.Data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region Load Balancers

        /// <summary>
        /// Lists all load balancers assigned to the IP
        /// 
        ///  <param name="server_id">Unique server's identifier.</param>
        ///  <param name="ip_id">Unique server's identifier.</param>
        /// </summary>
        public List<OneAndOne.POCO.Respones.Servers.LoadBalancers> GetLoadBalancer(string server_id, string ip_id)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/ips/{ip_id}/load_balancers", Method.GET);
                request.AddUrlSegment("server_id", server_id);
                request.AddUrlSegment("ip_id", ip_id);

                var result = restclient.Execute<List<OneAndOne.POCO.Respones.Servers.LoadBalancers>>(request);
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(result.Content);
                }
                return result.Data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Adds a new IP to the server.
        /// </summary>
        /// <param name="server_id">Unique server's identifier.</param>
        ///  <param name="ip_id">Unique server's identifier.</param>
        /// 
        public ServerResponse CreateLoadBalancer(string server_id, string ip_id, string loadBalancerId)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/ips/{ip_id}/load_balancers", Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("server_id", server_id);
                request.AddUrlSegment("ip_id", ip_id);
                string load_balancer_id = loadBalancerId;
                request.AddBody(new { load_balancer_id });
                var result = restclient.Execute<ServerResponse>(request);
                if (result.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new Exception(result.Content);
                }
                return result.Data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Releases an IP and optionally removes it
        /// </summary>
        /// <param name="server_id">Unique server's identifier.</param>
        /// <param name="ip_id">ip_id: required (string ) Unique IP's identifier.</param>
        /// <param name="keep_id">Set true for releasing the IP without removing it</param>
        /// 
        public ServerResponse DeleteLoadBalancer(string server_id, string ip_id, string load_balancer_id)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/ips/{ip_id}/load_balancers/{load_balancer_id}", Method.DELETE)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("server_id", server_id);
                request.AddUrlSegment("ip_id", ip_id);
                request.AddUrlSegment("load_balancer_id", load_balancer_id);
                request.AddHeader("Content-Type", "application/json");


                var result = restclient.Execute<ServerResponse>(request);
                if (result.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new Exception(result.Content);
                }
                return result.Data;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
    }
}
