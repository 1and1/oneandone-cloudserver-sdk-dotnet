using OneAndOne.Client.RESTHelpers;
using OneAndOne.POCO.Requests.PublicIPs;
using OneAndOne.POCO.Respones.PublicIPs;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.Client.Endpoints.PublicIPs
{
    public class PublicIPs : ResourceBase
    {

        public PublicIPs(object _apiUrl = null, object _apiKey = null)
            : base(_apiUrl, _apiKey) { }

        #region Basic Operations
        /// <summary>
        /// Returns a list of your public IPs.
        /// </summary>
        /// <param name="page">Allows to use pagination. Sets the number of servers that will be shown in each page.</param>
        /// <param name="perPage">Current page to show.</param>
        /// <param name="sort">Allows to sort the result by priority:sort=name retrieves a list of elements ordered by their names.sort=-creation_date retrieves a list of elements ordered according to their creation date in descending order of priority.</param>
        /// <param name="query">Allows to search one string in the response and return the elements that contain it. In order to specify the string use parameter q:    q=My server</param>
        /// <param name="fields">Returns only the parameters requested: fields=id,name,description,hardware.ram</param>

        public List<PublicIPsResponse> Get(int? page = null, int? perPage = null, string sort = null, string query = null, string fields = null)
        {
            try
            {
                string requestUrl = "/public_ips?";
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

                var result = restclient.Execute<List<PublicIPsResponse>>(request);
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
        ///Creates a new public IP.
        ///</summary>
        public PublicIPsResponse Create(CreatePublicIPRequest publicIP)
        {
            try
            {
                var request = new RestRequest("/public_ips", Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddBody(publicIP);

                var result = restclient.Execute<PublicIPsResponse>(request);
                if (result.StatusCode != HttpStatusCode.Created)
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
        /// Returns information about a public IP.
        /// </summary>
        /// <param name="ip_id">Unique IP's identifier.</param>
        /// 
        public PublicIPsResponse Show(string ip_id)
        {
            try
            {
                var request = new RestRequest("/public_ips/{ip_id}", Method.GET);
                request.AddUrlSegment("ip_id", ip_id);

                var result = restclient.Execute<PublicIPsResponse>(request);
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
        /// Modifies the reverse DNS of a public IP.
        /// </summary>
        /// <param name="ip_id">Unique IP's identifier.</param>
        public PublicIPsResponse Update(string reverseDns, string ip_id)
        {
            try
            {
                var request = new RestRequest("/public_ips/{ip_id}", Method.PUT)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("ip_id", ip_id);
                string reverse_dns = reverseDns;
                request.AddBody(new { reverse_dns });

                var result = restclient.Execute<PublicIPsResponse>(request);
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
        /// Removes a public IP.
        /// </summary>
        /// <param name="ip_id">Unique IP's identifier.</param>
        public PublicIPsResponse Delete(string ip_id)
        {
            try
            {
                var request = new RestRequest("/public_ips/{ip_id}", Method.DELETE)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("ip_id", ip_id);
                request.AddHeader("Content-Type", "application/json");

                var result = restclient.Execute<PublicIPsResponse>(request);
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
        #endregion
    }
}
