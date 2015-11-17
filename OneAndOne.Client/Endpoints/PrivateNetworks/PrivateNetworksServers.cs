using OneAndOne.Client.RESTHelpers;
using OneAndOne.POCO.Requests.PrivateNetworks;
using OneAndOne.POCO.Respones.PrivateNetworks;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.Client.Endpoints.PrivateNetworks
{
    public partial class PrivateNetworks
    {
        #region Basic Operations
        /// <summary>
        /// Returns a list of the servers attached to a private network.
        /// <param name="private_network_id">Unique private network's identifier.</param>
        /// </summary>
        public List<PrivateNetworkServerResponse> GetPrivateNetworkServers(string private_network_id)
        {
            try
            {
                var request = new RestRequest("/private_networks/{private_network_id}/servers", Method.GET);
                request.AddUrlSegment("private_network_id", private_network_id);

                var result = restclient.Execute<List<PrivateNetworkServerResponse>>(request);
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
        //Attaches servers to a private network.
        /// <param name="private_network_id">Unique private network's identifier.</param>
        //</summary>
        public PrivateNetworkServerResponse CreatePrivateNetworkServers(AttachPrivateNetworkServersRequest privateNetworkServers, string private_network_id)
        {
            try
            {
                var request = new RestRequest("/private_networks/{private_network_id}/servers", Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("private_network_id", private_network_id);
                request.AddHeader("Content-Type", "application/json");

                request.AddBody(privateNetworkServers);

                var result = restclient.Execute<PrivateNetworkServerResponse>(request);
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
        /// Returns information about a server attached to a private network.
        /// </summary>
        /// <param name="private_network_id">Unique private network's identifier.</param>
        /// <param name="server_id">Unique server's identifier.</param>
        ///// 
        public PrivateNetworkServerResponse ShowPrivateNetworkServer(string private_network_id, string server_id)
        {
            try
            {
                var request = new RestRequest("/private_networks/{private_network_id}/servers/{server_id}", Method.GET);
                request.AddUrlSegment("private_network_id", private_network_id);
                request.AddUrlSegment("server_id", server_id);

                var result = restclient.Execute<PrivateNetworkServerResponse>(request);
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
        /// Unattaches a server from a private network.
        /// </summary>
        /// <param name="private_network_id">Unique private network's identifier.</param>
        /// 
        public PrivateNetworksResponse DeletePrivateNetworksServer(string private_network_id, string server_id)
        {
            try
            {
                var request = new RestRequest("/private_networks/{private_network_id}/servers/{server_id}", Method.DELETE)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("private_network_id", private_network_id);
                request.AddUrlSegment("server_id", server_id);
                request.AddHeader("Content-Type", "application/json");

                var result = restclient.Execute<PrivateNetworksResponse>(request);
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
