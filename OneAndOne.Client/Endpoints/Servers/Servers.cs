using OneAndOne.Client.Endpoints;
using OneAndOne.Client.RESTHelpers;
using OneAndOne.POCO.Requests.Servers;
using OneAndOne.POCO.Respones.Servers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.Client
{
    public class Servers : ResourceBase
    {

        #region Basic Operations
        /// <summary>
        /// Returns a list of your servers.
        /// </summary>
        /// <param name="page">Allows to use pagination. Sets the number of servers that will be shown in each page.</param>
        /// <param name="perPage">Current page to show.</param>
        /// <param name="sort">Allows to sort the result by priority:sort=name retrieves a list of elements ordered by their names.sort=-creation_date retrieves a list of elements ordered according to their creation date in descending order of priority.</param>
        /// <param name="query">Allows to search one string in the response and return the elements that contain it. In order to specify the string use parameter q:    q=My server</param>
        /// <param name="fields">Returns only the parameters requested: fields=id,name,description,hardware.ram</param>
        public List<ServerResponse> Get(int? page = null, int? perPage = null, string sort = null, string query = null, string fields = null)
        {
            try
            {

                string requestUrl = "/servers?";

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
                var result = restclient.Execute<List<ServerResponse>>(request);
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
        /// Adds a new server.
        /// </summary>
        public CreateServerResponse Create(CreateServerRequest server)
        {
            try
            {
                var request = new RestRequest("/servers", Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddBody(server);
                var result = restclient.Execute<CreateServerResponse>(request);
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
        /// Adds a new server.
        /// </summary>
        public UpdateServerResponse Update(UpdateServerRequest server, string serverId)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}", Method.PUT)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("server_id", serverId);
                request.AddBody(server);
                var result = restclient.Execute<UpdateServerResponse>(request);
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
        /// Returns information about one flavour.
        /// </summary>
        /// <param name="server_id">server_id: required (string ), Unique server's identifier.</param>
        /// 
        public ServerResponse Show(string server_id)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}", Method.GET);
                request.AddUrlSegment("server_id", server_id);
                var result = restclient.Execute<ServerResponse>(request);
                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
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
        /// Removes a server.
        /// </summary>
        /// <param name="server_id">required (string ), Unique server's identifier.</param>
        /// <param name="keepsIp">Set true for keeping server IPs after deleting a server (false by default).</param>
        /// 
        public DeleteServerResponse Delete(string serverId, bool keepsIps)
        {
            try
            {
                var request = new RestRequest("/servers/{serverId}?keep_ips={keep_ips}", Method.DELETE)
                    {
                        RequestFormat = DataFormat.Json,
                        JsonSerializer = new CustomSerializer()
                    };
                request.AddUrlSegment("serverId", serverId);
                request.AddUrlSegment("keep_ips", keepsIps.ToString().ToLower());
                request.AddHeader("Content-Type", "application/json");

                var result = restclient.Execute<DeleteServerResponse>(request);
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



        #region Additional Operations
        /// <summary>
        /// Returns available flavours for fixed servers.
        /// </summary>
        public List<AvailableHardwareFlavour> GetAvailableFixedServers()
        {
            try
            {
                var request = new RestRequest("/servers/fixed_instance_sizes", Method.GET);
                var result = restclient.Execute<List<AvailableHardwareFlavour>>(request);
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
        /// Returns information about one flavour.
        /// </summary>
        /// <param name="fixedinstaceId">    fixed_instance_size_id: required (string ).</param>
        /// 
        public AvailableHardwareFlavour GetFlavorInformation(string fixedinstaceId)
        {
            try
            {
                var request = new RestRequest("/servers/fixed_instance_sizes/{fixed_instance_size_id}", Method.GET);
                request.AddUrlSegment("fixed_instance_size_id", fixedinstaceId);
                var result = restclient.Execute<AvailableHardwareFlavour>(request);
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
        #endregion

    }
}
