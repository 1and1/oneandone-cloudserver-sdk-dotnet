using OneAndOne.Client.RESTHelpers;
using OneAndOne.POCO.Requests.FirewallPolicies;
using OneAndOne.POCO.Respones;
using OneAndOne.POCO.Respones.FirewallPolicies;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.Client.Endpoints.FirewallPolicies
{
    public partial class FirewallPolicies
    {
        #region Basic Operations
        /// <summary>
        /// Returns a list of the servers/IPs attached to a firewall policy.
        /// <param name="firewall_id">Unique firewall's identifier.</param>
        /// </summary>
        public List<FirewallPolicyServerIPsResponse> GetFirewallPolicyServerIps(string firewall_id)
        {
            try
            {
                var request = new RestRequest("/firewall_policies/{firewall_id}/server_ips", Method.GET);
                request.AddUrlSegment("firewall_id", firewall_id);

                var result = restclient.Execute<List<FirewallPolicyServerIPsResponse>>(request);
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

        //<summary>
        //Assigns servers/IPs to a firewall policy.
        /// <param name="firewall_id">Unique firewall's identifier.</param>
        //</summary>
        public FirewallPolicyResponse CreateFirewallPolicyServerIPs(AssignFirewallServerIPRequest serverIps, string firewall_id)
        {
            try
            {
                var request = new RestRequest("/firewall_policies/{firewall_id}/server_ips", Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("firewall_id", firewall_id);
                request.AddBody(serverIps);

                var result = restclient.Execute<FirewallPolicyResponse>(request);
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
        /// Returns information about a server/IP assigned to a firewall policy.
        /// </summary>
        /// <param name="firewall_id">Unique firewall's identifier.</param>
        /// <param name="server_ip"> Unique IP's identifier.</param>
        ///// 
        public FirewallPolicyServerIPsResponse ShowFirewallPolicyServerIp(string firewall_id, string server_ip)
        {
            try
            {
                var request = new RestRequest("/firewall_policies/{firewall_id}/server_ips/{server_ip}", Method.GET);
                request.AddUrlSegment("firewall_id", firewall_id);
                request.AddUrlSegment("server_ip", server_ip);

                var result = restclient.Execute<FirewallPolicyServerIPsResponse>(request);
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
        /// Unattaches a server from a shared storage.
        /// </summary>
        /// <param name="firewall_id">Unique firewall's identifier.</param>
        /// <param name="server_ip"> Unique IP's identifier.</param>
        /// 
        public FirewallPolicyResponse DeleteFirewallPolicyServerIP(string firewall_id, string server_ip)
        {
            try
            {
                var request = new RestRequest("/firewall_policies/{firewall_id}/server_ips/{server_ip}", Method.DELETE)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("firewall_id", firewall_id);
                request.AddUrlSegment("server_ip", server_ip);
                request.AddHeader("Content-Type", "application/json");

                var result = restclient.Execute<FirewallPolicyResponse>(request);
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
