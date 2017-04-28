using Newtonsoft.Json;
using OneAndOne.POCO.Response.ServerAppliances;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.Client.Endpoints.ServerAppliances
{
    public class ServerAppliances : ResourceBase
    {
        public ServerAppliances(object _apiUrl = null, object _apiKey = null)
            : base(_apiUrl, _apiKey) { }
        /// <summary>
        /// Returns a list of all the appliances that you can use for creating a server..
        /// </summary>
        /// <param name="page">Allows to use pagination. Sets the number of servers that will be shown in each page.</param>
        /// <param name="perPage">Current page to show.</param>
        /// <param name="sort">Allows to sort the result by priority:sort=name retrieves a list of elements ordered by their names.sort=-creation_date retrieves a list of elements ordered according to their creation date in descending order of priority.</param>
        /// <param name="query">Allows to search one string in the response and return the elements that contain it. In order to specify the string use parameter q:    q=My server</param>
        /// <param name="fields">Returns only the parameters requested: fields=id,name,description,hardware.ram</param>
        public List<ServerAppliancesResponse> Get(int? page = null, int? perPage = null, string sort = null, string query = null, string fields = null)
        {
            try
            {
                string requestUrl = "/server_appliances?";
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

                var result = restclient.Execute(request);
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(result.Content);
                }
                return JsonConvert.DeserializeObject<List<ServerAppliancesResponse>>(result.Content);
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Information about specific appliance
        /// </summary>
        /// <param name="id">Appliance's ID</param>
        /// 
        public ServerAppliancesResponse Show(string id)
        {
            try
            {
                var request = new RestRequest("/server_appliances/{id}", Method.GET);
                request.AddUrlSegment("id", id);

                var result = restclient.Execute<ServerAppliancesResponse>(request);
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
    }
}
