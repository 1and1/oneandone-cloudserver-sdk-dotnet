using OneAndOne.Client.RESTHelpers;
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
    public partial class FirewallPolicies
    {
        #region Basic Operations
        /// <summary>
        /// Returns a list of the rules of a firewall policy.
        /// <param name="firewall_id">Unique firewall's identifier.</param>
        /// </summary>
        public List<FirewallRule> GetFirewallPolicyRules(string firewall_id)
        {
            try
            {
                var request = new RestRequest("/firewall_policies/{firewall_id}/rules", Method.GET);
                request.AddUrlSegment("firewall_id", firewall_id);

                var result = restclient.Execute<List<FirewallRule>>(request);
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
        //Adds new rules to a firewall policy.
        /// <param name="firewall_id">Unique firewall's identifier.</param>
        //</summary>
        public FirewallPolicyResponse CreateFirewallPolicyRule(AddFirewallPolicyRuleRequest rule, string firewall_id)
        {
            try
            {
                var request = new RestRequest("/firewall_policies/{firewall_id}/rules", Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("firewall_id", firewall_id);
                request.AddBody(rule);

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
        /// Returns information about a rule of a firewall policy.
        /// </summary>
        /// <param name="firewall_id">Unique firewall's identifier.</param>
        /// <param name="rule_id"> Unique rule's identifier.</param>
        ///// 
        public FirewallRule ShowFirewallPolicyRule(string firewall_id, string rule_id)
        {
            try
            {
                var request = new RestRequest("/firewall_policies/{firewall_id}/rules/{rule_id}", Method.GET);
                request.AddUrlSegment("firewall_id", firewall_id);
                request.AddUrlSegment("rule_id", rule_id);

                var result = restclient.Execute<FirewallRule>(request);
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
        /// Removes a rule from a firewall policy.
        /// </summary>
        /// <param name="firewall_id">Unique firewall's identifier.</param>
        /// <param name="rule_id">Unique rule's identifier.</param>
        /// 
        public FirewallPolicyResponse DeleteFirewallPolicyRules(string firewall_id, string rule_id)
        {
            try
            {
                var request = new RestRequest("/firewall_policies/{firewall_id}/rules/{rule_id}", Method.DELETE)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("firewall_id", firewall_id);
                request.AddUrlSegment("rule_id", rule_id);
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
