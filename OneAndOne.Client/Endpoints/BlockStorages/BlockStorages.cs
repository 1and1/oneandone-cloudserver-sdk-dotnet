using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using OneAndOne.Client.RESTHelpers;
using OneAndOne.POCO.Requests.BlockStorages;
using OneAndOne.POCO.Response.BlockStorages;
using RestSharp;

namespace OneAndOne.Client.Endpoints.BlockStorages
{
    public class BlockStorages : ResourceBase
    {
        public BlockStorages(object _apiUrl = null, object _apiKey = null) 
            : base(_apiUrl, _apiKey) { }

        #region Basic Operations

        /// <summary>
        /// Returns a list of your block storages.
        /// </summary>
        /// <param name="page">Allows to use pagination. Sets the number of block storages that will be shown in each page.</param>
        /// <param name="perPage">Current page to show.</param>
        /// <param name="sort">Allows to sort the result by priority:sort=name retrieves a list of elements ordered by their names.sort=-creation_date retrieves a list of elements ordered according to their creation date in descending order of priority.</param>
        /// <param name="query">Allows to search one string in the response and return the elements that contain it. In order to specify the string use parameter q: q=My block storage</param>
        /// <param name="fields">Returns only the parameters requested: fields=id,name,size</param>
        public List<BlockStoragesResponse> Get(int? page = null, int? perPage = null, string sort = null, string query = null, string fields = null)
        {
            try
            {
                StringBuilder requestUrl = new StringBuilder("/block_storages?");

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

                var result = restclient.Execute<List<BlockStoragesResponse>>(request);
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
        /// Adds a new block_storage.
        /// </summary>
        public BlockStoragesResponse Create(CreateBlockStorageRequest blockStorage)
        {
            try
            {
                var request = new RestRequest("/block_storages", Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddBody(blockStorage);

                var result = restclient.Execute<BlockStoragesResponse>(request);
                if (result.StatusCode != HttpStatusCode.Accepted && result.StatusCode != HttpStatusCode.Created)
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
        /// Returns information about a block storage.
        /// </summary>
        /// <param name="blockStorageId">Unique block storage's identifier.</param>
        public BlockStoragesResponse Show(string blockStorageId)
        {
            try
            {
                var request = new RestRequest("/block_storages/{block_storage_id}", Method.GET);
                request.AddUrlSegment("block_storage_id", blockStorageId);

                var result = restclient.Execute<BlockStoragesResponse>(request);
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
        /// Modifies a block storage.
        /// </summary>
        /// <param name="blockStorageId">Unique block storage's identifier.</param>
        public BlockStoragesResponse Update(UpdateBlockStorageRequest blockStorage, string blockStorageId)
        {
            try
            {
                var request = new RestRequest("/block_storages/{block_storage_id}", Method.PUT)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("block_storage_id", blockStorageId);
                request.AddBody(blockStorage);

                var result = restclient.Execute<BlockStoragesResponse>(request);
                if (result.StatusCode != HttpStatusCode.Accepted
                    && result.StatusCode != HttpStatusCode.OK)
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
        /// Removes a block storage.
        /// </summary>
        /// <param name="blockStorageId">Unique block storage's identifier.</param>
        public Object Delete(string blockStorageId)
        {
            try
            {
                var request = new RestRequest("/block_storages/{block_storage_id}", Method.DELETE)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("block_storage_id", blockStorageId);
                request.AddHeader("Content-Type", "application/json");

                var result = restclient.Execute<Object>(request);
                if (result.StatusCode != HttpStatusCode.Accepted
                    && result.StatusCode != HttpStatusCode.OK)
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

        #region Block Storages Server Operations

        /// <summary>
        /// Attaches a server to a block storage.
        /// </summary>
        /// <param name="blockStorageId">Unique block storage's identifier.</param>
        public BlockStoragesResponse CreateServerBlockStorage(BlockStorageServerRequest serverBlockStorage, string blockStorageId)
        {
            try
            {
                var request = new RestRequest("/block_storages/{block_storage_id}/server", Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("block_storage_id", blockStorageId);
                request.AddBody(serverBlockStorage);

                var result = restclient.Execute<BlockStoragesResponse>(request);
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
        /// Returns information about the server attached to the block storage.
        /// </summary>
        /// <param name="blockStorageId">Unique block storage's identifier.</param>
        public Server GetBlockStorageServer(string blockStorageId)
        {
            try
            {
                var request = new RestRequest("/block_storages/{block_storage_id}/server", Method.GET){
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("block_storage_id", blockStorageId);

                var result = restclient.Execute<Server>(request);
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
        /// Detaches a server from a block storage.
        /// </summary>
        /// <param name="blockStorageId">Unique block storage's identifier.</param>
        /// 
        public BlockStoragesResponse DeleteBlockStorageServer(string blockStorageId)
        {
            try
            {
                var request = new RestRequest("/block_storages/{block_storage_id}/server", Method.DELETE);
                request.AddUrlSegment("block_storage_id", blockStorageId);
                request.AddHeader("Content-Type", "application/json");

                var result = restclient.Execute<BlockStoragesResponse>(request);
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
