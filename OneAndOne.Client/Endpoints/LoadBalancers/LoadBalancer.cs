using Newtonsoft.Json;
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
    public partial class LoadBalancer : ResourceBase
    {

        public LoadBalancer(object _apiUrl = null, object _apiKey = null)
            : base(_apiUrl, _apiKey) { }

        #region Basic Operations
        /// <summary>
        /// Returns a list of your load balancers.
        /// </summary>
        /// <param name="page">Allows to use pagination. Sets the number of servers that will be shown in each page.</param>
        /// <param name="perPage">Current page to show.</param>
        /// <param name="sort">Allows to sort the result by priority:sort=name retrieves a list of elements ordered by their names.sort=-creation_date retrieves a list of elements ordered according to their creation date in descending order of priority.</param>
        /// <param name="query">Allows to search one string in the response and return the elements that contain it. In order to specify the string use parameter q:    q=My server</param>
        /// <param name="fields">Returns only the parameters requested: fields=id,name,description,hardware.ram</param>

        public List<OneAndOne.POCO.Response.LoadBalancers.LoadBalancerResponse> Get(int? page = null, int? perPage = null, string sort = null, string query = null, string fields = null)
        {
            try
            {
                StringBuilder requestUrl = new StringBuilder("/load_balancers?");
                if (page != null)
                {
                    requestUrl.AppendFormat("&page={0}", page);
                }
                if (perPage != null)
                {
                    requestUrl.AppendFormat("&per_page={0}", perPage);
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    requestUrl.AppendFormat("&sort={0}", sort);
                }
                if (!string.IsNullOrEmpty(query))
                {
                    requestUrl.AppendFormat("&q={0}", query);
                }
                if (!string.IsNullOrEmpty(fields))
                {
                    requestUrl.AppendFormat("&fields={0}", fields);
                }
                var request = new RestRequest(requestUrl.ToString(), Method.GET);

                var result = restclient.Execute(request);
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(result.Content);
                }
                return JsonConvert.DeserializeObject<List<OneAndOne.POCO.Response.LoadBalancers.LoadBalancerResponse>>(result.Content);

            }
            catch
            {
                throw;
            }
        }

        ///<summary>
        //Creates a new load balancer.
        //</summary>
        public OneAndOne.POCO.Response.LoadBalancers.LoadBalancerResponse Create(CreateLoadBalancerRequest loadBalancer)
        {
            try
            {
                var request = new RestRequest("/load_balancers", Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddBody(loadBalancer);

                var result = restclient.Execute<OneAndOne.POCO.Response.LoadBalancers.LoadBalancerResponse>(request);
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
        /// Returns information about a load balancer.
        /// </summary>
        /// <param name="load_balancer_id">Returns information about a load balancer.</param>
        /// 
        public OneAndOne.POCO.Response.LoadBalancers.LoadBalancerResponse Show(string load_balancer_id)
        {
            try
            {
                var request = new RestRequest("/load_balancers/{load_balancer_id}", Method.GET);
                request.AddUrlSegment("load_balancer_id", load_balancer_id);

                var result = restclient.Execute<OneAndOne.POCO.Response.LoadBalancers.LoadBalancerResponse>(request);
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
        /// Modifies a load balancer.
        /// </summary>
        /// <param name="load_balancer_id">Unique load balancer's identifier.</param>
        public OneAndOne.POCO.Response.LoadBalancers.LoadBalancerResponse Update(UpdateLoadBalancerRequest loadBalancer, string load_balancer_id)
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

                var result = restclient.Execute<OneAndOne.POCO.Response.LoadBalancers.LoadBalancerResponse>(request);
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
        /// Removes a load balancer.
        /// </summary>
        /// <param name="load_balancer_id">Unique load balancer's identifier.</param>
        /// 
        public OneAndOne.POCO.Response.LoadBalancers.LoadBalancerResponse Delete(string load_balancer_id)
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

                var result = restclient.Execute<OneAndOne.POCO.Response.LoadBalancers.LoadBalancerResponse>(request);
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
