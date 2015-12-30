using OneAndOne.Client.RESTHelpers;
using OneAndOne.POCO.Respones.MonitoringPolicies;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.Client.Endpoints.MonitoringPolicies
{
    public class MonitoringPoliciesServers : ResourceBase
    {

        public MonitoringPoliciesServers(object _apiUrl = null, object _apiKey = null)
            : base(_apiUrl, _apiKey) { }
        #region Basic Operations

        /// <summary>
        /// Returns a list of the servers attached to a monitoring policy.
        /// </summary>
        /// <param name="monitoring_policy_id">Unique monitoring policy's identifier.</param>
        /// 
        public List<Server> Get(string monitoring_policy_id)
        {
            try
            {
                var request = new RestRequest("/monitoring_policies/{monitoring_policy_id}/servers", Method.GET);
                request.AddUrlSegment("monitoring_policy_id", monitoring_policy_id);

                var result = restclient.Execute<List<Server>>(request);
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
        ///Attaches servers to a monitoring policy.
        /// <param name="monitoring_policy_id">Unique monitoring policy's identifier.</param>
        ///</summary>
        public MonitoringPoliciesResponse Create(List<string> servers, string monitoring_policy_id)
        {
            try
            {
                var request = new RestRequest("/monitoring_policies/{monitoring_policy_id}/servers", Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };

                request.AddUrlSegment("monitoring_policy_id", monitoring_policy_id);
                request.AddBody(new { servers = servers });

                var result = restclient.Execute<MonitoringPoliciesResponse>(request);
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
        /// Returns information about a server attached to a monitoring policy.
        /// </summary>
        /// <param name="monitoring_policy_id">Unique monitoring policy's identifier.</param>
        /// <param name="server_id">Unique server's identifier.</param>
        /// 
        public Server Show(string monitoring_policy_id, string server_id)
        {
            try
            {
                var request = new RestRequest("/monitoring_policies/{monitoring_policy_id}/servers/{server_id}", Method.GET);
                request.AddUrlSegment("monitoring_policy_id", monitoring_policy_id);
                request.AddUrlSegment("server_id", server_id);

                var result = restclient.Execute<Server>(request);
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
        /// Unattaches a server from a monitoring policy.
        /// </summary>
        /// <param name="server_id">Unique server's identifier.</param>
        /// <param name="monitoring_policy_id">Unique monitoring policy's identifier.</param>
        /// 
        public MonitoringPoliciesResponse Delete(string monitoring_policy_id, string server_id)
        {
            try
            {
                var request = new RestRequest("/monitoring_policies/{monitoring_policy_id}/servers/{server_id}", Method.DELETE)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("monitoring_policy_id", monitoring_policy_id);
                request.AddUrlSegment("server_id", server_id);
                request.AddHeader("Content-Type", "application/json");

                var result = restclient.Execute<MonitoringPoliciesResponse>(request);
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
