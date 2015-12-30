using OneAndOne.Client.RESTAuth;
using OneAndOne.POCO.Respones;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.Client.Endpoints
{
    public class ResourceBase
    {
        public RestClient restclient;
        string APIKEY = ConfigurationManager.AppSettings["APIToken"];
        string APIURL = ConfigurationManager.AppSettings["APIURL"];
        string usedAPIURL;
        string usedAPIKey;
        public ResourceBase(object _apiUrl = null, object _apiKey = null)
        {
            try
            {
                usedAPIURL = _apiUrl != null && !string.IsNullOrEmpty(_apiUrl.ToString()) ? _apiUrl.ToString() : APIURL;
                usedAPIKey = _apiUrl != null && !string.IsNullOrEmpty(_apiKey.ToString()) ? _apiKey.ToString() : APIKEY;
                if (string.IsNullOrEmpty(usedAPIURL))
                {
                    throw new Exception("Missing API URL you can either pass it or add it to your config file with key name 'APIURL'");
                }

                if (string.IsNullOrEmpty(usedAPIKey))
                {
                    throw new Exception("Missing APIToken you can either pass it or add it to your config file with key name 'APIToken'");
                }
                restclient = new RestClient(usedAPIURL)
                    {
                        Authenticator = new OneAndOneAuthenticator(usedAPIKey)
                    };
            }
            catch (UriFormatException)
            {
                throw new Exception(string.Format("the provided URI '{0}' is not correct, please replace with a valid URI either in the config file or in the constructor of the OneAndOneClient", usedAPIURL));
            }
        }
    }
}
