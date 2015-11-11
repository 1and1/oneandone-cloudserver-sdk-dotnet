using Newtonsoft.Json;
using OneAndOne.Client.RESTHelpers;
using OneAndOne.POCO.Converters;
using OneAndOne.POCO.Requests.Servers;
using OneAndOne.POCO.Respones.Servers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.Client.Endpoints.Servers
{
    public class Hardware : ResourceBase
    {
        #region Server Hardware Operations
        /// <summary>
        /// Returns information about the server's hardware.
        /// </summary>
        /// <param name="server_id">server_id: required (string ), Unique server's identifier.</param>
        /// 
        public OneAndOne.POCO.Respones.Servers.Hardware Show(string server_id)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/hardware", Method.GET);
                request.AddUrlSegment("server_id", server_id);
                var result = restclient.Execute<OneAndOne.POCO.Respones.Servers.Hardware>(request);
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
        /// Modifies the server's hardware.
        /// </summary>
        /// <param name="server_id">server_id: required (string ), Unique server's identifier.</param>
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
                if (result.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
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

        #region DVD

        /// <summary>
        /// Returns information about the DVD loaded into the virtual DVD unit of a server.
        /// </summary>
        /// <param name="server_id">server_id: required (string ), Unique server's identifier.</param>
        /// 
        public OneAndOne.POCO.Respones.Servers.Dvd ShowDVD(string server_id)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/dvd", Method.GET);
                request.AddUrlSegment("server_id", server_id);
                var result = restclient.Execute<OneAndOne.POCO.Respones.Servers.Dvd>(request);
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
        /// Loads a DVD into the virtual DVD unit of a server.
        /// </summary>
        /// <param name="server_id">server_id: required (string ), Unique server's identifier.</param>
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
            catch (Exception ex)
            {
                throw;
            }
        }


        /// <summary>
        /// Unloads a DVD from the virtual DVD unit of a server.
        /// </summary>
        /// <param name="server_id">server_id: required (string ), Unique server's identifier.</param>
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
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
    }
}
