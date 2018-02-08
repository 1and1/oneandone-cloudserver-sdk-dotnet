using OneAndOne.Client.RESTHelpers;
using OneAndOne.POCO.Requests.Servers;
using OneAndOne.POCO.Response.Servers;
using RestSharp;
using System;
using System.Net;

namespace OneAndOne.Client.Endpoints.Servers
{
    public class ServersHardware : ResourceBase
    {
        public ServersHardware(object _apiUrl = null, object _apiKey = null)
            : base(_apiUrl, _apiKey) { }

        #region Server Hardware Operations
        /// <summary>
        /// Returns information about the server's hardware.
        /// </summary>
        /// <param name="server_id">Unique server's identifier.</param>
        /// 
        public OneAndOne.POCO.Response.Servers.ServerHardware Show(string server_id)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/hardware", Method.GET);
                request.AddUrlSegment("server_id", server_id);

                var result = restclient.Execute<OneAndOne.POCO.Response.Servers.ServerHardware>(request);
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
        /// Modifies the server's hardware.
        /// </summary>
        /// <param name="server_id">Unique server's identifier.</param>
        /// 
        public ServerResponse Update(UpdateHardwareRequest hardware, string server_id)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/hardware", Method.PUT)
                    {
                        RequestFormat = DataFormat.Json,
                        JsonSerializer = new CustomSerializer()
                    };
                request.AddUrlSegment("server_id", server_id);
                request.AddBody(hardware);

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

        #region DVD

        /// <summary>
        /// Returns information about the DVD loaded into the virtual DVD unit of a server.
        /// <param name="server_id">Unique server's identifier.</param>
        /// </summary>
        public OneAndOne.POCO.Response.Servers.Dvd ShowDVD(string server_id)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/dvd", Method.GET);
                request.AddUrlSegment("server_id", server_id);
                var result = restclient.Execute<OneAndOne.POCO.Response.Servers.Dvd>(request);
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
        /// Loads a DVD into the virtual DVD unit of a server.
        /// </summary>
        /// <param name="server_id">Unique server's identifier.</param>
        /// 
        public ServerResponse UpdateDVD(string server_id, string dvd_id)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/dvd", Method.PUT)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("server_id", server_id);
                request.AddHeader("Content-Type", "application/json");

                string id = dvd_id;
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
        /// Unloads a DVD from the virtual DVD unit of a server.
        /// </summary>
        /// <param name="server_id">Unique server's identifier.</param>
        /// 
        public ServerResponse DeleteDVD(string server_id)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/dvd", Method.DELETE)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddHeader("Content-Type", "application/json");
                request.AddUrlSegment("server_id", server_id);

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
