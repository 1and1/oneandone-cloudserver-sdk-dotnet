using OneAndOne.Client.RESTHelpers;
using OneAndOne.POCO.Requests.Vpn;
using OneAndOne.POCO.Response.Vpn;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.Client.Endpoints.Vpn
{
    public class Vpn : ResourceBase
    {
        public Vpn(object _apiUrl = null, object _apiKey = null)
            : base(_apiUrl, _apiKey) { }
        #region Basic Operations
        /// <summary>
        /// Returns a list of your VPN.
        /// </summary>
        /// <param name="page">Allows to use pagination. Sets the number of servers that will be shown in each page.</param>
        /// <param name="perPage">Current page to show.</param>
        /// <param name="sort">Allows to sort the result by priority:sort=name retrieves a list of elements ordered by their names.sort=-creation_date retrieves a list of elements ordered according to their creation date in descending order of priority.</param>
        /// <param name="query">Allows to search one string in the response and return the elements that contain it. In order to specify the string use parameter q:    q=My server</param>
        /// <param name="fields">Returns only the parameters requested: fields=id,name,description,hardware.ram</param>
        public List<VpnResponse> Get(int? page = null, int? perPage = null, string sort = null, string query = null, string fields = null)
        {
            try
            {
                string requestUrl = "/vpns?";
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

                var result = restclient.Execute<List<VpnResponse>>(request);
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
        //Adds a new VPN.
        //</summary>
        public VpnResponse Create(CreateVpnRequest vpn)
        {
            try
            {
                var request = new RestRequest("/vpns", Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddBody(vpn);

                var result = restclient.Execute<VpnResponse>(request);
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
        /// Download your VPN configuration file.
        /// </summary>
        /// <param name="server_id">Unique server's identifier.</param>
        /// 
        public Config ShowConfiguration(string vpnId)
        {
            try
            {
                var request = new RestRequest("/vpns/{vpn_id}/configuration_file", Method.GET);
                request.AddUrlSegment("vpn_id", vpnId);

                var result = restclient.Execute<Config>(request);
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
        /// Returns information about one vpn.
        /// </summary>
        /// <param name="server_id">Unique server's identifier.</param>
        /// 
        public VpnResponse Show(string vpnId)
        {
            try
            {
                var request = new RestRequest("/vpns/{vpn_id}", Method.GET);
                request.AddUrlSegment("vpn_id", vpnId);

                var result = restclient.Execute<VpnResponse>(request);
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
        /// Modify VPN configuration file.
        /// </summary>
        public VpnResponse Update(UpdateVpnRequest vpn, string vpnId)
        {
            try
            {
                var request = new RestRequest("/vpns/{vpn_id}", Method.PUT)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("vpn_id", vpnId);
                request.AddBody(vpn);

                var result = restclient.Execute<VpnResponse>(request);
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
        /// Removes a VPN.
        /// </summary>
        /// <param name="vpn_id">required (string ), Unique vpn's identifier.</param>
        /// 
        public VpnResponse Delete(string vpn_id)
        {
            try
            {
                var request = new RestRequest("/vpns/{vpn_id}", Method.DELETE)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("vpn_id", vpn_id);
                request.AddHeader("Content-Type", "application/json");

                var result = restclient.Execute<VpnResponse>(request);
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
