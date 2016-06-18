using OneAndOne.Client.RESTHelpers;
using OneAndOne.POCO.Response.Users;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.Client.Endpoints.Users
{
    public class UserAPI : ResourceBase
    {
        public UserAPI(object _apiUrl = null, object _apiKey = null)
            : base(_apiUrl, _apiKey) { }
        #region Basic API Operations
        ///<summary>
        ///Information about API.
        ///</summary>
        public ApiResponse ShowUserAPI(string user_id)
        {
            try
            {
                var request = new RestRequest("/users/{user_id}/api", Method.GET);
                request.AddUrlSegment("user_id", user_id);

                var result = restclient.Execute<ApiResponse>(request);
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
        /// Allows to enable or disable the API.
        /// </summary>
        /// <param name="user_id">Unique user's identifier.</param>
        public UserResponse UpdateUserAPI(string user_id, bool active)
        {
            try
            {
                var request = new RestRequest("/users/{user_id}/api", Method.PUT)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("user_id", user_id);
                request.AddBody(new { active = active });

                var result = restclient.Execute<UserResponse>(request);
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

        #region Basic API KEY Operations
        ///<summary>
        ///Shows the API key
        ///</summary>
        public UserAPIKeyResponse ShowUserAPIKey(string user_id)
        {
            try
            {
                var request = new RestRequest("/users/{user_id}/api/key", Method.GET);
                request.AddUrlSegment("user_id", user_id);

                var result = restclient.Execute<UserAPIKeyResponse>(request);
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
        /// Changes the API key
        /// </summary>
        /// <param name="user_id">Unique user's identifier.</param>
        public UserResponse UpdateAPIKey(string user_id)
        {
            try
            {
                var request = new RestRequest("/users/{user_id}/api/key", Method.PUT)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("user_id", user_id);
                request.AddHeader("Content-Type", "application/json");

                var result = restclient.Execute<UserResponse>(request);
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

        #region  IPs Operations
        ///<summary>
        ///IP's from which access to API is allowed.
        ///</summary>
        public List<string> GetUserIps(string user_id)
        {
            try
            {
                var request = new RestRequest("/users/{user_id}/api/ips", Method.GET);
                request.AddUrlSegment("user_id", user_id);

                var result = restclient.Execute<List<string>>(request);
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
        /// Allows a new IP
        /// </summary>
        /// <param name="user_id">Unique user's identifier.</param>
        public UserResponse UpdateAPIIps(List<string> ips, string user_id)
        {
            try
            {
                var request = new RestRequest("/users/{user_id}/api/ips", Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddHeader("Content-Type", "application/json");
                request.AddUrlSegment("user_id", user_id);
                request.AddBody(new { ips = ips });

                var result = restclient.Execute<UserResponse>(request);
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
        /// Deletes an IP and forbides API access for it.
        /// </summary>
        /// <param name="user_id">Unique user's identifier.</param>
        /// <param name="ip">Desired IP</param>
        /// 
        public UserResponse DeleteUserIp(string user_id, string ip)
        {
            try
            {
                var request = new RestRequest("/users/{user_id}/api/ips/{ip}", Method.DELETE)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("user_id", user_id);
                request.AddUrlSegment("ip", ip);
                request.AddHeader("Content-Type", "application/json");

                var result = restclient.Execute<UserResponse>(request);
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
