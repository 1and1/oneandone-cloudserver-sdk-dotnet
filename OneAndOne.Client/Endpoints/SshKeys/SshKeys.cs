using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using OneAndOne.Client.RESTHelpers;
using OneAndOne.POCO.Requests.SshKeys;
using OneAndOne.POCO.Response.SshKeys;
using RestSharp;
using System.Text;

namespace OneAndOne.Client.Endpoints.SshKeys
{
    public class SshKeys : ResourceBase
    {
        public SshKeys(object _apiUrl = null, object _apiKey = null) 
            : base(_apiUrl, _apiKey) { }

        #region Basic Operations

        /// <summary>
        /// Returns a list of your ssh_keys.
        /// </summary>
        /// <param name="page">Allows to use pagination. Sets the number of servers that will be shown in each page.</param>
        /// <param name="perPage">Current page to show.</param>
        /// <param name="sort">Allows to sort the result by priority:sort=name retrieves a list of elements ordered by their names.sort=-creation_date retrieves a list of elements ordered according to their creation date in descending order of priority.</param>
        /// <param name="query">Allows to search one string in the response and return the elements that contain it. In order to specify the string use parameter q: q=My ssh key</param>
        /// <param name="fields">Returns only the parameters requested: fields=id,name,description,...</param>
        public List<SshKeyResponse> Get(int? page = null, int? perPage = null, string sort = null, string query = null, string fields = null)
        {
            try
            {
                StringBuilder requestUrl = new StringBuilder("/ssh_keys?");
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

                var result = restclient.Execute<List<SshKeyResponse>>(request);
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
        ///Adds a new ssh_key.
        ///</summary>
        public SshKeyResponse Create(CreateSshKeyRequest sshKey)
        {
            try
            {
                var request = new RestRequest("/ssh_keys", Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddBody(sshKey);

                var result = restclient.Execute<SshKeyResponse>(request);
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
        /// Returns information about one ssh_key.
        /// </summary>
        /// <param name="sshKeyId">Unique ssh keys's identifier.</param>
        public SshKeyResponse Show(string sshKeyId)
        {
            try
            {
                var request = new RestRequest("/ssh_keys/{ssh_key_id}", Method.GET);
                request.AddUrlSegment("ssh_key_id", sshKeyId);

                var result = restclient.Execute<SshKeyResponse>(request);
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
        /// Modifies a ssh_key.
        /// </summary>
        public SshKeyResponse Update(UpdateSshKeyRequest sshKey, string sshKeyId)
        {
            try
            {
                var request = new RestRequest("/ssh_keys/{ssh_key_id}", Method.PUT)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("ssh_key_id", sshKeyId);
                request.AddBody(sshKey);

                var result = restclient.Execute<SshKeyResponse>(request);
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
        /// Removes a ssh_key.
        /// </summary>
        /// <param name="sshKeyId">Unique ssh key's identifier.</param>
        /// 
        public SshKeyResponse Delete(string sshKeyId)
        {
            try
            {
                var request = new RestRequest("/ssh_keys/{ssh_key_id}", Method.DELETE)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("ssh_key_id", sshKeyId);
                request.AddHeader("Content-Type", "application/json");

                var result = restclient.Execute<SshKeyResponse>(request);
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
