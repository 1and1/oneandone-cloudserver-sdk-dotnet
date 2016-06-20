using OneAndOne.Client.RESTHelpers;
using OneAndOne.POCO.Requests.LoadBalancer;
using OneAndOne.POCO.Response.LoadBalancers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.Client.Endpoints.LoadBalancers
{
    public partial class LoadBalancer
    {
        #region Basic Operations
        /// <summary>
        /// Returns a list of the servers/IPs attached to a load balancer.
        /// </summary>
        /// <param name="load_balancer_id">Unique load balancer's identifier.</param>
        public List<LoadBalancerServerIpsResponse> GetLoadBalancerServerIps(string load_balancer_id)
        {
            try
            {
                var request = new RestRequest("/load_balancers/{load_balancer_id}/server_ips", Method.GET);
                request.AddUrlSegment("load_balancer_id", load_balancer_id);

                var result = restclient.Execute<List<LoadBalancerServerIpsResponse>>(request);
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(result.Content);
                }
                return result.Data;
            }
            catch
            {
                throw;
            }
        }

        ///<summary>
        ///Assigns servers/IPs to a load balancer.
        ///</summary>
        /// <param name="load_balancer_id">Unique load balancer's identifier.</param>
        public LoadBalancerResponse CreateLoadBalancerServerIPs(AssignLoadBalancerServerIpsRequest serverIps, string load_balancer_id)
        {
            try
            {
                var request = new RestRequest("/load_balancers/{load_balancer_id}/server_ips", Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("load_balancer_id", load_balancer_id);
                request.AddBody(serverIps);

                var result = restclient.Execute<LoadBalancerResponse>(request);
                if (result.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new Exception(result.Content);
                }
                return result.Data;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Returns information about a server/IP assigned to a load balancer.
        /// </summary>
        /// <param name="load_balancer_id">Unique load balancer's identifier.</param>
        /// <param name="server_ip"> Unique IP's identifier.</param>
        /// 
        public LoadBalancerResponse ShowLoadBalancerServerIp(string load_balancer_id, string server_ip)
        {
            try
            {
                var request = new RestRequest("/load_balancers/{load_balancer_id}/server_ips/{server_ip}", Method.GET);
                request.AddUrlSegment("load_balancer_id", load_balancer_id);
                request.AddUrlSegment("server_ip", server_ip);

                var result = restclient.Execute<LoadBalancerResponse>(request);
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(result.Content);
                }
                return result.Data;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Unassigns a server/IP from a load balancer.
        /// </summary>
        /// <param name="load_balancer_id">Unique firewall's identifier.</param>
        /// <param name="server_ip"> Unique IP's identifier.</param>
        /// 
        public LoadBalancerResponse DeleteLoadBalancerServerIP(string load_balancer_id, string server_ip)
        {
            try
            {
                var request = new RestRequest("/load_balancers/{load_balancer_id}/server_ips/{server_ip}", Method.DELETE)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("load_balancer_id", load_balancer_id);
                request.AddUrlSegment("server_ip", server_ip);
                request.AddHeader("Content-Type", "application/json");

                var result = restclient.Execute<LoadBalancerResponse>(request);
                if (result.StatusCode != HttpStatusCode.Accepted)
                {
                    throw new Exception(result.Content);
                }
                return result.Data;
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
