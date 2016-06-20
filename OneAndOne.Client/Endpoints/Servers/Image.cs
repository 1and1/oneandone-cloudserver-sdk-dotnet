using OneAndOne.Client.RESTHelpers;
using OneAndOne.POCO.Requests.Servers;
using OneAndOne.POCO.Response.Servers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.Client.Endpoints.Servers
{
    public class ServerImage : ResourceBase
    {

        public ServerImage(object _apiUrl = null, object _apiKey = null)
            : base(_apiUrl, _apiKey) { }
        /// <summary>
        /// Returns information about a server's image.
        /// </summary>
        ///  <param name="server_id">Unique server's identifier.</param>
        public OneAndOne.POCO.Response.Servers.Image Show(string server_id)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/image", Method.GET);
                request.AddUrlSegment("server_id", server_id);

                var result = restclient.Execute<OneAndOne.POCO.Response.Servers.Image>(request);
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
        /// Reinstalls a new image into a server.
        /// </summary>
        /// <param name="server_id">Unique server's identifier.</param>
        /// 
        public ServerResponse Update(UpdateServerImageRequest image, string server_id)
        {
            try
            {
                var request = new RestRequest("/servers/{server_id}/image", Method.PUT)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new CustomSerializer()
                };
                request.AddUrlSegment("server_id", server_id);
                request.AddBody(image);

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
