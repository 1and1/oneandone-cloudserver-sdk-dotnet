using Newtonsoft.Json;
using OneAndOne.POCO.Response.Common;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.Client.Endpoints.Common
{
    public class Common : ResourceBase
    {
        public Common(object _apiUrl = null, object _apiKey = null)
            : base(_apiUrl, _apiKey) { }
        /// <summary>
        /// Returns prices for all available resources in Cloud Panel
        /// </summary>
        public PricingResponse GetPricing()
        {
            try
            {
                var request = new RestRequest("/pricing", Method.GET);

                var result = restclient.Execute(request);
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(result.Content);
                }
                return JsonConvert.DeserializeObject<PricingResponse>(result.Content);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Returns true if the API is running
        /// </summary>
        public bool Ping()
        {
            try
            {
                bool value = false;
                var request = new RestRequest("/ping", Method.GET);

                var result = restclient.Execute(request);
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    return value;
                }
                if (result.Content == "[\"PONG\"]")
                { value = true; }
                return value;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Returns true if the API is running and the token is valid
        /// </summary>
        public bool PingAuthentication()
        {
            try
            {
                bool value = false;
                var request = new RestRequest("/ping_auth", Method.GET);

                var result = restclient.Execute(request);
                if (result.StatusCode != HttpStatusCode.OK)
                {
                    return value;
                }
                if (result.Content == "[\"PONG\"]")
                { value = true; }
                return value;
            }
            catch
            {
                return false;
            }
        }

    }
}
