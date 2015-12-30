﻿using OneAndOne.Client.RESTHelpers;
using OneAndOne.POCO.Requests.FirewallPolicies;
using OneAndOne.POCO.Respones;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.Client.Endpoints.FirewallPolicies
{
    public partial class FirewallPolicies : ResourceBase
    {
        public FirewallPolicies(object _apiUrl = null, object _apiKey = null)
            : base(_apiUrl, _apiKey) { }
        #region Basic Operations
        /// <summary>
        /// Returns a list of your firewall policies.
        /// </summary>
        /// <param name="page">Allows to use pagination. Sets the number of servers that will be shown in each page.</param>
        /// <param name="perPage">Current page to show.</param>
        /// <param name="sort">Allows to sort the result by priority:sort=name retrieves a list of elements ordered by their names.sort=-creation_date retrieves a list of elements ordered according to their creation date in descending order of priority.</param>
        /// <param name="query">Allows to search one string in the response and return the elements that contain it. In order to specify the string use parameter q:    q=My server</param>
        /// <param name="fields">Returns only the parameters requested: fields=id,name,description,hardware.ram</param>
        public List<FirewallPolicyResponse> Get(int? page = null, int? perPage = null, string sort = null, string query = null, string fields = null)
        {
            try
            {
                string requestUrl = "/firewall_policies?";
                if (page != null)
                {
                    requestUrl += string.Format("&page={0}", page);
                }
                if (perPage != null)
                {
                    requestUrl += string.Format("&per_page={0}", perPage);
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    requestUrl += string.Format("&sort={0}", sort);
                }
                if (!string.IsNullOrEmpty(query))
                {
                    requestUrl += string.Format("&q={0}", query);
                }
                if (!string.IsNullOrEmpty(fields))
                {
                    requestUrl += string.Format("&fields={0}", fields);
                }
                var request = new RestRequest(requestUrl, Method.GET);

                var result = restclient.Execute<List<FirewallPolicyResponse>>(request);
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
        //Creates a new firewall policy.
        //</summary>
        public FirewallPolicyResponse Create(CreateFirewallPolicyRequest policy)
        {
            try
            {
                var request = new RestRequest("/firewall_policies", Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddBody(policy);

                var result = restclient.Execute<FirewallPolicyResponse>(request);
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
        /// Returns information about a firewall policy.
        /// </summary>
        /// <param name="firewall_id"> ,Unique firewall's identifier</param>
        /// 
        public FirewallPolicyResponse Show(string firewall_id)
        {
            try
            {
                var request = new RestRequest("/firewall_policies/{firewall_id}", Method.GET);
                request.AddUrlSegment("firewall_id", firewall_id);

                var result = restclient.Execute<FirewallPolicyResponse>(request);
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
        /// Modifies a firewall policy.
        /// </summary>
        /// <param name="firewall_id">Unique firewall's identifier.</param>
        /// 
        public FirewallPolicyResponse Update(UpdateFirewallPolicyRequest firewallPolicy, string firewall_id)
        {
            try
            {
                var request = new RestRequest("/firewall_policies/{firewall_id}", Method.PUT)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("firewall_id", firewall_id);
                request.AddBody(firewallPolicy);

                var result = restclient.Execute<FirewallPolicyResponse>(request);
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
        /// Removes a firewall policy.
        /// </summary>
        /// <param name="firewall_id">Unique firewall's identifier.</param>
        /// 
        public FirewallPolicyResponse Delete(string firewall_id)
        {
            try
            {
                var request = new RestRequest("/firewall_policies/{firewall_id}", Method.DELETE)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("firewall_id", firewall_id);
                request.AddHeader("Content-Type", "application/json");

                var result = restclient.Execute<FirewallPolicyResponse>(request);
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
