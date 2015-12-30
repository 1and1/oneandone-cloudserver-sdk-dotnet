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
    public class MonitoringPoliciesPorts : ResourceBase
    {
        public MonitoringPoliciesPorts(object _apiUrl = null, object _apiKey = null)
            : base(_apiUrl, _apiKey) { }
        #region Basic Operations

        /// <summary>
        /// Returns a list of the ports of a monitoring policy.
        /// </summary>
        /// <param name="monitoring_policy_id">Unique monitoring policy's identifier.</param>
        /// 
        public List<Ports> Get(string monitoring_policy_id)
        {
            try
            {
                var request = new RestRequest("/monitoring_policies/{monitoring_policy_id}/ports", Method.GET);
                request.AddUrlSegment("monitoring_policy_id", monitoring_policy_id);

                var result = restclient.Execute<List<Ports>>(request);
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
        //Adds new ports to a monitoring policy
        //</summary>
        /// <param name="monitoring_policy_id">Unique monitoring policy's identifier.</param>
        public MonitoringPoliciesResponse Create(List<OneAndOne.POCO.Requests.MonitoringPolicies.Ports> ports, string monitoring_policy_id)
        {
            try
            {
                var request = new RestRequest("/monitoring_policies/{monitoring_policy_id}/ports", Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("monitoring_policy_id", monitoring_policy_id);
                request.AddBody(new { ports = ports });

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
        /// Returns information about a port of a monitoring policy.
        /// </summary>
        /// <param name="monitoring_policy_id">Unique monitoring policy's identifier.</param>
        /// <param name="port_id">Unique port's identifier.</param>
        /// 
        public Ports Show(string monitoring_policy_id, string port_id)
        {
            try
            {
                var request = new RestRequest("/monitoring_policies/{monitoring_policy_id}/ports/{port_id}", Method.GET);
                request.AddUrlSegment("monitoring_policy_id", monitoring_policy_id);
                request.AddUrlSegment("port_id", port_id);

                var result = restclient.Execute<Ports>(request);
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
        /// Modifies a port from a monitoring policy.
        /// </summary>
        /// <param name="port_id">Unique port's identifier.</param>
        /// <param name="monitoring_policy_id">Unique monitoring policy's identifier.</param>
        public MonitoringPoliciesResponse Update(OneAndOne.POCO.Requests.MonitoringPolicies.Ports ports, string monitoring_policy_id, string port_id)
        {
            try
            {
                var request = new RestRequest("/monitoring_policies/{monitoring_policy_id}/ports/{port_id}", Method.PUT)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("monitoring_policy_id", monitoring_policy_id);
                request.AddUrlSegment("port_id", port_id);
                request.AddBody(new { ports = ports });

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
        /// Removes a port from a monitoring policy.
        /// </summary>
        /// <param name="port_id">Unique port's identifier.</param>
        /// <param name="monitoring_policy_id">Unique monitoring policy's identifier.</param>
        /// 
        public MonitoringPoliciesResponse Delete(string monitoring_policy_id, string port_id)
        {
            try
            {
                var request = new RestRequest("/monitoring_policies/{monitoring_policy_id}/ports/{port_id}", Method.DELETE)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("monitoring_policy_id", monitoring_policy_id);
                request.AddUrlSegment("port_id", port_id);
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
