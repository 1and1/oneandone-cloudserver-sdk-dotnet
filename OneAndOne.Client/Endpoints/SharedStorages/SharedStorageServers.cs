using Newtonsoft.Json;
using OneAndOne.Client.RESTHelpers;
using OneAndOne.POCO.Requests.SharedStorages;
using OneAndOne.POCO.Respones.SharedStorages;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.Client.Endpoints.SharedStorages
{
    public partial class SharedStorages
    {

        #region Basic Operations
        /// <summary>
        /// Returns a list of the servers attached to a shared storage.
        /// </summary>
        /// <param name="shared_storage_id">Unique shared storage's identifier.</param>
        public List<SharedStorageServerResponse> GetSharedStorageServers(string shared_storage_id)
        {
            try
            {
                var request = new RestRequest("/shared_storages/{shared_storage_id}/servers", Method.GET);
                request.AddUrlSegment("shared_storage_id", shared_storage_id);

                var result = restclient.Execute<List<SharedStorageServerResponse>>(request);
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
        ///Attaches servers to a shared storage.
        ///</summary>
        /// <param name="shared_storage_id">Unique shared storage's identifier.</param>
        public SharedStoragesResponse CreateServerSharedStorages(AttachSharedStorageServerRequest serverSharedStorage, string shared_storage_id)
        {
            try
            {
                var request = new RestRequest("/shared_storages/{shared_storage_id}/servers", Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("shared_storage_id", shared_storage_id);
                request.AddBody(serverSharedStorage);

                var result = restclient.Execute<SharedStoragesResponse>(request);
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
        /// Returns information about a shared storage.
        /// </summary>
        /// <param name="shared_storage_id">Unique shared storage's identifier.</param>
        /// <param name="server_id">Unique server's identifier.</param>
        /// 
        public SharedStorageServerResponse ShowSharedStoragesServer(string shared_storage_id, string server_id)
        {
            try
            {
                var request = new RestRequest("/shared_storages/{shared_storage_id}/servers/{server_id}", Method.GET);
                request.AddUrlSegment("shared_storage_id", shared_storage_id);
                request.AddUrlSegment("server_id", server_id);

                var result = restclient.Execute<SharedStorageServerResponse>(request);
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
        /// Unattaches a server from a shared storage.
        /// </summary>
        /// <param name="shared_storage_id">Unique shared storage's identifier.</param>
        /// 
        public SharedStoragesResponse DeleteSharedStoragesServer(string shared_storage_id, string server_id)
        {
            try
            {
                var request = new RestRequest("/shared_storages/{shared_storage_id}/servers/{server_id}", Method.DELETE)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("shared_storage_id", shared_storage_id);
                request.AddUrlSegment("server_id", server_id);
                request.AddHeader("Content-Type", "application/json");

                var result = restclient.Execute<SharedStoragesResponse>(request);
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
