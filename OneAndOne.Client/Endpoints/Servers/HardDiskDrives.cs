using OneAndOne.Client.RESTHelpers;
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
    public class ServerHdds : ResourceBase
    {

        public ServerHdds(object _apiUrl = null, object _apiKey = null)
            : base(_apiUrl, _apiKey) { }

        /// <summary>
        /// Returns information about a server's hard disk.
        /// </summary>
        /// <param name="server_id">Unique server's identifier.</param>
        /// <param name="hdd_id">hdd_id: Unique hard disk's identifier. </param>
        public Hdd Show(string server_id, string hdd_id)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/hardware/hdds/{hdd_id}", Method.GET);
                request.AddUrlSegment("server_id", server_id);
                request.AddUrlSegment("hdd_id", hdd_id);

                var result = restclient.Execute<Hdd>(request);
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
        /// Returns a list of the server's hard disks.
        /// </summary>
        /// <param name="server_id">Unique server's identifier.</param>
        public List<Hdd> Get(string server_id)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/hardware/hdds", Method.GET);
                request.AddUrlSegment("server_id", server_id);

                var result = restclient.Execute<List<Hdd>>(request);
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
        /// Adds new hard disk(s) to the server.
        /// </summary>
        /// <param name="server_id">Unique server's identifier.</param>
        public ServerResponse Create(AddHddRequest hdds, string server_id)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/hardware/hdds", Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("server_id", server_id);
                request.AddBody(hdds);

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
        /// Adds new hard disk(s) to the server.
        /// </summary>
        /// <param name="server_id">Unique server's identifier.</param>
        public ServerResponse Update(UpdateHddRequest hdds, string server_id, string hdd_id)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/hardware/hdds/{hdd_id}", Method.PUT)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("server_id", server_id);
                request.AddUrlSegment("hdd_id", hdd_id);
                request.AddBody(hdds);

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
        /// Removes a server's hard disk.
        /// </summary>
        /// <param name="server_id">Unique server's identifier.</param>
        /// <param name="hdd_id">hdd_id: Unique hard disk's identifier.</param>
        public ServerResponse Delete(string server_id, string hdd_id)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/hardware/hdds/{hdd_id}", Method.DELETE)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("server_id", server_id);
                request.AddUrlSegment("hdd_id", hdd_id);
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
    }
}
