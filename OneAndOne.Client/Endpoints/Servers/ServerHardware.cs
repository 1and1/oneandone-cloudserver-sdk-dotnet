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
    public class ServerHardware : ResourceBase
    {

        /// <summary>
        /// Returns information about the server's hardware.
        /// </summary>
        /// <param name="server_id">server_id: required (string ), Unique server's identifier.</param>
        /// 
        public Hardware Show(string server_id)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/hardware", Method.GET);
                request.AddUrlSegment("server_id", server_id);
                var result = restclient.Execute<Hardware>(request);
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
    }
}
