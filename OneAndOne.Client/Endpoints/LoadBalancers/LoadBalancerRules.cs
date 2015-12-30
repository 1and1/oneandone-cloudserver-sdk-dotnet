using OneAndOne.Client.RESTHelpers;
using OneAndOne.POCO.Requests.LoadBalancer;
using OneAndOne.POCO.Respones.LoadBalancers;
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
        /// Returns a list of the rules of a load balancer.
        /// </summary>
        /// <param name="load_balancer_id">Unique load balancer's identifier.</param>
        public List<LoadBalancerRulesResponse> GetLoadBalancerRules(string load_balancer_id)
        {
            try
            {
                var request = new RestRequest("/load_balancers/{load_balancer_id}/rules", Method.GET);
                request.AddUrlSegment("load_balancer_id", load_balancer_id);

                var result = restclient.Execute<List<LoadBalancerRulesResponse>>(request);
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
        ///Adds new rules to a load balancer.
        ///</summary>
        /// <param name="load_balancer_id">Unique load balancer's identifier.</param>
        public LoadBalancerResponse CreateLoadBalancerRule(AddLoadBalancerRuleRequest rule, string load_balancer_id)
        {
            try
            {
                var request = new RestRequest("/load_balancers/{load_balancer_id}/rules", Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("load_balancer_id", load_balancer_id);
                request.AddBody(rule);

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
        /// Returns information about a rule of a load balancer.
        /// </summary>
        /// <param name="load_balancer_id">Unique load balancer's identifier.</param>
        /// <param name="rule_id"> Unique rule's identifier.</param>
        /// 
        public LoadBalancerRulesResponse ShowLoadBalancerRule(string load_balancer_id, string rule_id)
        {
            try
            {
                var request = new RestRequest("/load_balancers/{load_balancer_id}/rules/{rule_id}", Method.GET);
                request.AddUrlSegment("load_balancer_id", load_balancer_id);
                request.AddUrlSegment("rule_id", rule_id);

                var result = restclient.Execute<LoadBalancerRulesResponse>(request);
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
        /// Removes a rule from a load balancer.
        /// </summary>
        /// <param name="load_balancer_id">Unique load balancer's identifier..</param>
        /// <param name="rule_id">Unique rule's identifier.</param>
        /// 
        public LoadBalancerResponse DeleteLoadBalancerRules(string load_balancer_id, string rule_id)
        {
            try
            {
                var request = new RestRequest("/load_balancers/{load_balancer_id}/rules/{rule_id}", Method.DELETE)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("load_balancer_id", load_balancer_id);
                request.AddUrlSegment("rule_id", rule_id);
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
