using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.Client.Endpoints.DVDs
{
    public class DVDs : ResourceBase
    {

        /// <summary>
        /// Returns a list of the server's IPs.
        /// </summary>
        /// 
        public List<OneAndOne.POCO.Respones.DVDS.DVDResponse> Get()
        {
            try
            {
                var request = new RestRequest("/dvd_isos", Method.GET);
                var result = restclient.Execute<List<OneAndOne.POCO.Respones.DVDS.DVDResponse>>(request);
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
