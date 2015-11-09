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
    public class IPs : ResourceBase
    {
        /// <summary>
        /// Returns a list of the server's IPs.
        /// 
        ///  <param name="server_id">server_id: required (string ), Unique server's identifier.</param>
        /// </summary>
        public List<OneAndOne.POCO.Respones.Servers.IpAddress> Get(string server_id)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/ips", Method.GET);
                request.AddUrlSegment("server_id", server_id);

                var result = restclient.Execute<List<OneAndOne.POCO.Respones.Servers.IpAddress>>(request);
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
        /// Adds a new IP to the server.
        /// </summary>
        /// <param name="server_id">server_id: required (string ), Unique server's identifier.</param>
        /// 
        public ServerResponse Create(CreateServerIPRequest ip, string server_id)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/ips", Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("server_id", server_id);
                request.AddBody(ip);
                var result = restclient.Execute<ServerResponse>(request);
                if (result.StatusCode != HttpStatusCode.Created)
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
    }
}
