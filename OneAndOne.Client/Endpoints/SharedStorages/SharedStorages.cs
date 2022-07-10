using Newtonsoft.Json;
using OneAndOne.Client.RESTHelpers;
using OneAndOne.POCO.Requests.SharedStorages;
using OneAndOne.POCO.Response.SharedStorages;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.Client.Endpoints.SharedStorages
{
    public partial class SharedStorages : ResourceBase
    {
        public SharedStorages(object _apiUrl = null, object _apiKey = null)
            : base(_apiUrl, _apiKey) { }

        #region Basic Operations
        /// <summary>
        /// Returns a list of your shared storages.
        /// </summary>
        /// <param name="page">Allows to use pagination. Sets the number of servers that will be shown in each page.</param>
        /// <param name="perPage">Current page to show.</param>
        /// <param name="sort">Allows to sort the result by priority:sort=name retrieves a list of elements ordered by their names.sort=-creation_date retrieves a list of elements ordered according to their creation date in descending order of priority.</param>
        /// <param name="query">Allows to search one string in the response and return the elements that contain it. In order to specify the string use parameter q:    q=My server</param>
        /// <param name="fields">Returns only the parameters requested: fields=id,name,description,hardware.ram</param>
        public List<SharedStoragesResponse> Get(int? page = null, int? perPage = null, string sort = null, string query = null, string fields = null)
        {
            try
            {
                StringBuilder requestUrl = new StringBuilder("/shared_storages?");
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

                var result = restclient.Execute<List<SharedStoragesResponse>>(request);
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
        //Creates a new shared storage.
        //</summary>
        public SharedStoragesResponse Create(CreateSharedStorage sharedStorage)
        {
            try
            {
                var request = new RestRequest("/shared_storages", Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddBody(sharedStorage);

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
        /// 
        public SharedStoragesResponse Show(string shared_storage_id)
        {
            try
            {
                var request = new RestRequest("/shared_storages/{shared_storage_id}", Method.GET);
                request.AddUrlSegment("shared_storage_id", shared_storage_id);

                var result = restclient.Execute<SharedStoragesResponse>(request);
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
        /// Modifies a shared storage.
        /// </summary>
        /// <param name="shared_storage_id">Unique shared storage's identifier.</param>
        public SharedStoragesResponse Update(UpdateSharedStorageRequest sharedStorage, string shared_storage_id)
        {
            try
            {
                var request = new RestRequest("/shared_storages/{shared_storage_id}", Method.PUT)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("shared_storage_id", shared_storage_id);
                request.AddBody(sharedStorage);

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
        /// Removes a shared storage.
        /// </summary>
        /// <param name="shared_storage_id">Unique shared storage's identifier.</param>
        public SharedStoragesResponse Delete(string shared_storage_id)
        {
            try
            {
                var request = new RestRequest("/shared_storages/{shared_storage_id}", Method.DELETE)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("shared_storage_id", shared_storage_id);
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

        #region Shared Storage Access Operations

        /// <summary>
        /// Returns the credentials for accessing the shared storages.
        /// </summary>
        public List<SharedStorageAccessResponse> ShowSharedStorageAccess()
        {
            try
            {
                var request = new RestRequest("/shared_storages/access", Method.GET);

                var result = restclient.Execute<List<SharedStorageAccessResponse>>(request);
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
        /// Changes the password for accessing the shared storages.
        /// </summary>
        /// <param name="password">password</param>
        public List<SharedStorageAccessResponse> UpdateSharedStorageAccess(string password)
        {
            try
            {
                var request = new RestRequest("/shared_storages/access", Method.PUT)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddBody(new { password });

                var result = restclient.Execute<List<SharedStorageAccessResponse>>(request);
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
