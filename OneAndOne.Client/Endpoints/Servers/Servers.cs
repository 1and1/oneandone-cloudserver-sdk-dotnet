using OneAndOne.Client.Endpoints;
using OneAndOne.Client.RESTHelpers;
using OneAndOne.POCO.Requests.Servers;
using OneAndOne.POCO.Response.Servers;
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

        public Servers(object _apiUrl = null, object _apiKey = null)
            : base(_apiUrl, _apiKey) { }


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
                    requestUrl += string.Format("&q={0}", query);
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
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Returns a list of your baremetal servers.
        /// </summary>
        /// <param name="page">Allows to use pagination. Sets the number of servers that will be shown in each page.</param>
        /// <param name="perPage">Current page to show.</param>
        /// <param name="sort">Allows to sort the result by priority:sort=name retrieves a list of elements ordered by their names.sort=-creation_date retrieves a list of elements ordered according to their creation date in descending order of priority.</param>
        /// <param name="query">Allows to search one string in the response and return the elements that contain it. In order to specify the string use parameter q:    q=My server</param>
        /// <param name="fields">Returns only the parameters requested: fields=id,name,description,hardware.ram</param>
        public List<ServerResponse> GetBaremetal(int? page = null, int? perPage = null, string sort = null, string query = null, string fields = null)
        {
            try
            {

                string requestUrl = "/servers/baremetal_models?";

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
                var result = restclient.Execute<List<ServerResponse>>(request);
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
            catch
            {
                throw;
            }

        }


        /// <summary>
        /// Adds a new server.
        /// </summary>
        public CreateServerResponse CreateServerFromFlavor(CreateServerWithFlavorRequest server)
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
            catch
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
            catch
            {
                throw;
            }

        }



        /// <summary>
        /// Returns server's information.
        /// </summary>
        /// <param name="server_id">Unique server's identifier.</param>
        /// 
        public ServerResponse Show(string server_id)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}", Method.GET);
                request.AddUrlSegment("server_id", server_id);

                var result = restclient.Execute<ServerResponse>(request);
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
        /// Returns baremetal server's information.
        /// </summary>
        /// <param name="server_id">Unique server's identifier.</param>
        /// 
        public ServerResponse ShowBaremetal(string server_id)
        {
            try
            {
                var request = new RestRequest("/servers/baremetal_models/{server_id}", Method.GET);
                request.AddUrlSegment("server_id", server_id);

                var result = restclient.Execute<ServerResponse>(request);
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
        /// Removes a server.
        /// </summary>
        /// <param name="serverId">required (string ), Unique server's identifier.</param>
        /// <param name="keepsIps">Set true for keeping server IPs after deleting a server (false by default).</param>
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
            catch
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
            catch
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
            catch
            {
                throw;
            }
        }
        #endregion

        #region Status
        /// <summary>
        /// Returns a list of the server's IPs.
        /// </summary>
        ///  <param name="server_id">Unique server's identifier.</param>
        public OneAndOne.POCO.Response.Servers.Status GetStatus(string server_id)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/status", Method.GET);
                request.AddUrlSegment("server_id", server_id);

                var result = restclient.Execute<OneAndOne.POCO.Response.Servers.Status>(request);
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
        /// Returns a list of the server's IPs.
        /// </summary>
        ///  <param name="server_id">Unique server's identifier.</param>
        /// 
        public ServerResponse UpdateStatus(UpdateStatusRequest body, string server_id)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/status/action", Method.PUT)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("server_id", server_id);
                request.AddHeader("Content-Type", "application/json");
                request.AddBody(body);

                var result = restclient.Execute<ServerResponse>(request);
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

        #region Private Networks

        /// <summary>
        /// Returns a list of the server's private networks.
        /// </summary>
        ///  <param name="server_id">Unique server's identifier.</param>
        public List<PrivateNetworks> GetPrivateNetworks(string server_id)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/private_networks", Method.GET);
                request.AddUrlSegment("server_id", server_id);

                var result = restclient.Execute<List<PrivateNetworks>>(request);
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
        /// Returns information about a server's private network.
        /// </summary>
        ///  <param name="server_id">Unique server's identifier.</param>
        ///  <param name="privateNetworkId">private_network_id: required (string ), Unique private network's identifier.</param>
        public OneAndOne.POCO.Response.PrivateNetworks.PrivateNetworksResponse ShowPrivateNetworks(string server_id, string privateNetworkId)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/private_networks/{private_network_id}", Method.GET);
                request.AddUrlSegment("server_id", server_id);
                request.AddUrlSegment("private_network_id", privateNetworkId);

                var result = restclient.Execute<OneAndOne.POCO.Response.PrivateNetworks.PrivateNetworksResponse>(request);
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
        /// Assigns a private network to the server.
        /// </summary>
        public ServerResponse CreatePrivateNetwork(string server_id, string privateNetworkId)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/private_networks", Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("server_id", server_id);
                string id = privateNetworkId;
                request.AddBody(new { id });

                var result = restclient.Execute<ServerResponse>(request);
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
        /// Unassigns a private network from the server.
        /// </summary>
        /// <param name="server_id">Unique server's identifier.</param>
        public ServerResponse DeletePrivateNetwork(string server_id, string privateNetworkId)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/private_networks/{private_network_id}", Method.DELETE)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddHeader("Content-Type", "application/json");
                request.AddUrlSegment("server_id", server_id);
                request.AddUrlSegment("private_network_id", privateNetworkId);

                var result = restclient.Execute<ServerResponse>(request);
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

        #region Snapshot


        /// <summary>
        /// Returns a list of the server's snapshots.
        /// </summary>
        ///  <param name="server_id">Unique server's identifier.</param>
        public List<Snapshots> GetSnapshots(string server_id)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/snapshots", Method.GET);
                request.AddUrlSegment("server_id", server_id);

                var result = restclient.Execute<List<Snapshots>>(request);
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
        /// Restores a snapshot into the server.
        /// </summary>
        ///  <param name="server_id">Unique server's identifier.</param>
        ///  <param name="snapshotId">private_network_id: required (string ), Unique private network's identifier.</param>
        public ServerResponse UpdateSnapshot(string server_id, string snapshotId)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/snapshots/{snapshot_id}", Method.PUT)
                {
                    JsonSerializer = new CustomSerializer(),
                    RequestFormat = DataFormat.Json

                };
                request.AddUrlSegment("server_id", server_id);
                request.AddUrlSegment("snapshot_id", snapshotId);

                request.AddHeader("Content-Type", "application/json");


                var result = restclient.Execute<ServerResponse>(request);
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
        /// Creates a new snapshot of the server.
        /// </summary>
        public ServerResponse CreateSnapshot(string server_id)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/snapshots", Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("server_id", server_id);
                request.AddHeader("Content-Type", "application/json");


                var result = restclient.Execute<ServerResponse>(request);
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
        /// Removes a snapshot
        /// </summary>
        /// <param name="server_id">Unique server's identifier.</param>
        /// <param name="snapshot_id">Unique snapshot's identifier.</param>
        /// 
        public ServerResponse DeleteSnapshot(string server_id, string snapshotId)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/snapshots/{snapshot_id}", Method.DELETE)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddHeader("Content-Type", "application/json");
                request.AddUrlSegment("server_id", server_id);
                request.AddUrlSegment("snapshot_id", snapshotId);

                var result = restclient.Execute<ServerResponse>(request);
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
        /// Clones a server.
        /// </summary>
        public ServerResponse CreateClone(string server_id, string cloneName)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/clone", Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("server_id", server_id);
                string publicName = cloneName;
                request.AddBody(new { publicName, name = publicName });

                var result = restclient.Execute<ServerResponse>(request);
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
