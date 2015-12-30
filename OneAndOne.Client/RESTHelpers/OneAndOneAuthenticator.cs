using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.Client.RESTAuth
{
    public class OneAndOneAuthenticator : IAuthenticator
    {
        /// <summary>
        /// _apiToken obtained from oneandone Control Panel
        /// </summary>
        private readonly string _apiToken;

        public OneAndOneAuthenticator(string apiToken)
        {
            _apiToken = apiToken;
        }

        /// <summary>
        /// Adds the API token key to the REST request header
        /// </summary>
        public void Authenticate(RestSharp.IRestClient client, RestSharp.IRestRequest request)
        {
            if (!request.Parameters.Any(p => p.Name.Equals("X-TOKEN", StringComparison.OrdinalIgnoreCase)))
            {
                var token = string.Format("{0}", _apiToken);
                request.AddParameter("X-TOKEN", token, ParameterType.HttpHeader);
            }
        }
    }
}
