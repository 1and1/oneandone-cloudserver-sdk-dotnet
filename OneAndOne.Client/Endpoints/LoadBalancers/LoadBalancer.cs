using OneAndOne.Client.RESTHelpers;
using OneAndOne.POCO.Requests.LoadBalancer;
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

        #region Basic Operations
        /// <summary>
        /// Returns a list of your load balancers.
        /// </summary>
        /// <param name="page">Allows to use pagination. Sets the number of servers that will be shown in each page.</param>
        /// <param name="perPage">Current page to show.</param>
        /// <param name="sort">Allows to sort the result by priority:sort=name retrieves a list of elements ordered by their names.sort=-creation_date retrieves a list of elements ordered according to their creation date in descending order of priority.</param>
        /// <param name="query">Allows to search one string in the response and return the elements that contain it. In order to specify the string use parameter q:    q=My server</param>
        /// <param name="fields">Returns only the parameters requested: fields=id,name,description,hardware.ram</param>

        public List<OneAndOne.POCO.Respones.LoadBalancers.LoadBalancerResponse> Get(int? page = null, int? perPage = null, string sort = null, string query = null, string fields = null)
        {
            try
            {
                string requestUrl = "/load_balancers?";
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
                    requestUrl += string.Format("&query={0}", query);
                }
                if (!string.IsNullOrEmpty(fields))
                {
                    requestUrl += string.Format("&fields={0}", fields);
                }
                var request = new RestRequest(requestUrl, Method.GET);

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

        //<summary>
        //Creates a new load balancer.
        //</summary>
        public OneAndOne.POCO.Respones.LoadBalancers.LoadBalancerResponse Create(CreateLoadBalancerRequest loadBalancer)
        {
            try
            {
                var request = new RestRequest("/load_balancers", Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddBody(loadBalancer);

                var result = restclient.Execute<OneAndOne.POCO.Respones.LoadBalancers.LoadBalancerResponse>(request);
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
        /// Returns information about a load balancer.
        /// </summary>
        /// <param name="load_balancer_id">Returns information about a load balancer.</param>
        ///// 
        public OneAndOne.POCO.Respones.LoadBalancers.LoadBalancerResponse Show(string load_balancer_id)
        {
            try
            {
                var request = new RestRequest("/load_balancers/{load_balancer_id}", Method.GET);
                request.AddUrlSegment("load_balancer_id", load_balancer_id);

                var result = restclient.Execute<OneAndOne.POCO.Respones.LoadBalancers.LoadBalancerResponse>(request);
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

        ///// <summary>
        /// Modifies a load balancer.
        /// <param name="load_balancer_id">Unique load balancer's identifier.</param>
        /// </summary>
        public OneAndOne.POCO.Respones.LoadBalancers.LoadBalancerResponse Update(UpdateLoadBalancerRequest loadBalancer, string load_balancer_id)
        {
            try
            {
                var request = new RestRequest("/load_balancers/{load_balancer_id}", Method.PUT)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("load_balancer_id", load_balancer_id);
                request.AddBody(loadBalancer);

                var result = restclient.Execute<OneAndOne.POCO.Respones.LoadBalancers.LoadBalancerResponse>(request);
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
        /// Removes a load balancer.
        /// </summary>
        /// <param name="load_balancer_id">Unique load balancer's identifier.</param>
        /// 
        public OneAndOne.POCO.Respones.LoadBalancers.LoadBalancerResponse Delete(string load_balancer_id)
        {
            try
            {
                var request = new RestRequest("/load_balancers/{load_balancer_id}", Method.DELETE)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("load_balancer_id", load_balancer_id);
                request.AddHeader("Content-Type", "application/json");

                var result = restclient.Execute<OneAndOne.POCO.Respones.LoadBalancers.LoadBalancerResponse>(request);
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
