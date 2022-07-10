using OneAndOne.Client.RESTHelpers;
using OneAndOne.POCO.Requests.Users;
using OneAndOne.POCO.Response.Roles;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.Client.Endpoints.Roles
{
    public class Roles : ResourceBase
    {
        public Roles(object _apiUrl = null, object _apiKey = null)
            : base(_apiUrl, _apiKey) { }
        #region Basic Operations
        /// <summary>
        /// Returns a list with all roles
        /// </summary>
        /// <param name="page">Allows to use pagination. Sets the number of servers that will be shown in each page.</param>
        /// <param name="perPage">Current page to show.</param>
        /// <param name="sort">Allows to sort the result by priority:sort=name retrieves a list of elements ordered by their names.sort=-creation_date retrieves a list of elements ordered according to their creation date in descending order of priority.</param>
        /// <param name="query">Allows to search one string in the response and return the elements that contain it. In order to specify the string use parameter q:    q=My server</param>
        /// <param name="fields">Returns only the parameters requested: fields=id,name,description,hardware.ram</param>
        public List<RoleResponse> Get(int? page = null, int? perPage = null, string sort = null, string query = null, string fields = null)
        {
            try
            {
                StringBuilder requestUrl = new StringBuilder("/roles?");
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

                var result = restclient.Execute<List<RoleResponse>>(request);
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
        //Creates a new role.
        //</summary>
        public RoleResponse Create(string roleName)
        {
            try
            {
                var request = new RestRequest("/roles", Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddBody(new
                {
                    name = roleName
                });

                var result = restclient.Execute<RoleResponse>(request);
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
        /// Returns information about a role.
        /// </summary>
        /// <param name="role_id">Unique role's identifier.</param>
        /// 
        public RoleResponse Show(string role_id)
        {
            try
            {
                var request = new RestRequest("/roles/{role_id}", Method.GET);
                request.AddUrlSegment("role_id", role_id);

                var result = restclient.Execute<RoleResponse>(request);
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
        /// Modifies role information.
        /// </summary>
        /// <param name="roleId">Unique role's identifier.</param>
        public RoleResponse Update(string name, string description, UserState state, string role_id)
        {
            try
            {
                var request = new RestRequest("/roles/{role_id}", Method.PUT)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("role_id", role_id);
                request.AddBody(new
                {
                    name = name,
                    description = description,
                    state = state
                });

                var result = restclient.Execute<RoleResponse>(request);
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
        /// Removes a role.
        /// </summary>
        /// <param name="role_id">Unique role's identifier.</param>
        /// 
        public RoleResponse Delete(string role_id)
        {
            try
            {
                var request = new RestRequest("/roles/{role_id}", Method.DELETE)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("role_id", role_id);
                request.AddHeader("Content-Type", "application/json");

                var result = restclient.Execute<RoleResponse>(request);
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

        #region role permissions 
        /// <summary>
        /// Lists role's permissions.
        /// </summary>
        /// <param name="role_id">Unique role's identifier.</param>
        /// 
        public Permissions GetPermissions(string role_id)
        {
            try
            {
                var request = new RestRequest("/roles/{role_id}/permissions", Method.GET);
                request.AddUrlSegment("role_id", role_id);

                var result = restclient.Execute<Permissions>(request);
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
        /// Adds permissions to the role.
        /// </summary>
        /// <param name="roleId">Unique role's identifier.</param>
        public RoleResponse UpdatePermissions(Permissions permissions, string role_id)
        {
            try
            {
                var request = new RestRequest("/roles/{role_id}/permissions", Method.PUT)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("role_id", role_id);
                request.AddBody(permissions);

                var result = restclient.Execute<RoleResponse>(request);
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

        #region roles users

        ///<summary>
        //Returns users assigned to role
        /// <param name="role_id">Unique role's identifier.</param>
        //</summary>
        public List<OneAndOne.POCO.Response.Roles.User> GetRoleUsers(string role_id)
        {
            try
            {
                var request = new RestRequest("/roles/{role_id}/users", Method.GET);
                request.AddUrlSegment("role_id", role_id);

                var result = restclient.Execute<List<OneAndOne.POCO.Response.Roles.User>>(request);
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
        //Add users to role
        /// <param name="role_id">Unique role's identifier.</param>
        //</summary>
        public RoleResponse CreateRoleUsers(List<String> users, string role_id)
        {
            try
            {
                var request = new RestRequest("/roles/{role_id}/users", Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("role_id", role_id);
                request.AddBody(new { users = users });

                var result = restclient.Execute<RoleResponse>(request);
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
        /// Returns information about a user.
        /// </summary>
        /// <param name="role_id">Unique role's identifier.</param>
        /// <param name="user_id">Unique users's identifier.</param>
        /// 
        public OneAndOne.POCO.Response.Roles.User ShowRoleUser(string role_id, string user_id)
        {
            try
            {
                var request = new RestRequest("/roles/{role_id}/users/{user_id}", Method.GET);
                request.AddUrlSegment("role_id", role_id);
                request.AddUrlSegment("user_id", user_id);

                var result = restclient.Execute<OneAndOne.POCO.Response.Roles.User>(request);
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
        /// Removes user from role.
        /// </summary>
        /// <param name="role_id">Unique role's identifier.</param>
        /// <param name="user_id">Unique users's identifier.</param>
        /// 
        public RoleResponse DeleteRoleUser(string role_id, string user_id)
        {
            try
            {
                var request = new RestRequest("/roles/{role_id}/users/{user_id}", Method.DELETE)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("role_id", role_id);
                request.AddUrlSegment("user_id", user_id);

                request.AddHeader("Content-Type", "application/json");

                var result = restclient.Execute<RoleResponse>(request);
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

        ///<summary>
        //Clones a role
        /// <param name="role_id">Unique role's identifier.</param>
        //</summary>
        public RoleResponse CreateRoleClone(string name, string role_id)
        {
            try
            {
                var request = new RestRequest("/roles/{role_id}/clone", Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("role_id", role_id);
                request.AddBody(new { name = name });

                var result = restclient.Execute<RoleResponse>(request);
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
        #endregion
    }
}

