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
    public class MonitoringPoliciesProcesses : ResourceBase
    {
        public MonitoringPoliciesProcesses(object _apiUrl = null, object _apiKey = null)
            : base(_apiUrl, _apiKey) { }
        #region Basic Operations

        /// <summary>
        /// Returns a list of the processes of a monitoring policy.
        /// </summary>
        /// <param name="monitoring_policy_id">Unique monitoring policy's identifier.</param>
        public List<Processes> Get(string monitoring_policy_id)
        {
            try
            {
                var request = new RestRequest("/monitoring_policies/{monitoring_policy_id}/processes", Method.GET);
                request.AddUrlSegment("monitoring_policy_id", monitoring_policy_id);

                var result = restclient.Execute<List<Processes>>(request);
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
        //Adds new processes to a monitoring policy
        //</summary>
        /// <param name="monitoring_policy_id">Unique monitoring policy's identifier.</param>

        public MonitoringPoliciesResponse Create(List<OneAndOne.POCO.Requests.MonitoringPolicies.Processes> processes, string monitoring_policy_id)
        {
            try
            {
                var request = new RestRequest("/monitoring_policies/{monitoring_policy_id}/processes", Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("monitoring_policy_id", monitoring_policy_id);
                request.AddBody(new { processes = processes });

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
        /// Returns information about a process of a monitoring policy.
        /// </summary>
        /// <param name="monitoring_policy_id">Unique monitoring policy's identifier.</param>
        /// <param name="process_id">Unique process's identifier.</param>
        /// 
        public Processes Show(string monitoring_policy_id, string process_id)
        {
            try
            {
                var request = new RestRequest("/monitoring_policies/{monitoring_policy_id}/processes/{process_id}", Method.GET);
                request.AddUrlSegment("monitoring_policy_id", monitoring_policy_id);
                request.AddUrlSegment("process_id", process_id);

                var result = restclient.Execute<Processes>(request);
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
        /// Modifies a process from a monitoring policy.
        /// </summary>
        /// <param name="process_id">Unique process's identifier.</param>
        /// <param name="monitoring_policy_id">Unique monitoring policy's identifier.</param>
        public MonitoringPoliciesResponse Update(OneAndOne.POCO.Requests.MonitoringPolicies.Processes processes, string monitoring_policy_id, string process_id)
        {
            try
            {
                var request = new RestRequest("/monitoring_policies/{monitoring_policy_id}/processes/{process_id}", Method.PUT)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("monitoring_policy_id", monitoring_policy_id);
                request.AddUrlSegment("process_id", process_id);
                request.AddBody(new { processes = processes });

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
        /// Removes a process from a monitoring policy.
        /// </summary>
        /// <param name="process_id">Unique process's identifier.</param>
        /// <param name="monitoring_policy_id">Unique monitoring policy's identifier.</param>
        /// 
        public MonitoringPoliciesResponse Delete(string monitoring_policy_id, string process_id)
        {
            try
            {
                var request = new RestRequest("/monitoring_policies/{monitoring_policy_id}/processes/{process_id}", Method.DELETE)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("monitoring_policy_id", monitoring_policy_id);
                request.AddUrlSegment("process_id", process_id);
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
