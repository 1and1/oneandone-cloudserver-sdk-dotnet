using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.Client.Endpoints.LoadBalancers
{
    //TODO :refactor objects 
    public class LoadBalancer : ResourceBase
    {
        /// <summary>
        /// Returns a list of your load balancers.
        /// </summary>
        /// 
        public List<OneAndOne.POCO.Respones.LoadBalancers.LoadBalancerResponse> Get()
        {
            try
            {
                var request = new RestRequest("/load_balancers", Method.GET);
                var result = restclient.Execute<List<OneAndOne.POCO.Respones.LoadBalancers.LoadBalancerResponse>>(request);
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
    }
}
