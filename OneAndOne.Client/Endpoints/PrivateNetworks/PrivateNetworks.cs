using OneAndOne.POCO.Respones.PrivateNetworks;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.Client.Endpoints.PrivateNetworks
{
    //TODO :refactor objects 
    public class PrivateNetworks : ResourceBase
    {

        /// <summary>
        /// Returns a list of your private networks.
        /// </summary>
        public List<PrivateNetworksResponse> GetPrivateNetworks()
        {
            try
            {
                var request = new RestRequest("/private_networks", Method.GET);

                var result = restclient.Execute<List<PrivateNetworksResponse>>(request);
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
