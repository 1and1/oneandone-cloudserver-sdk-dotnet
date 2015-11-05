using OneAndOne.Client.RESTAuth;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.Client.Endpoints
{
    public class EndpointsBase
    {
        public RestClient restclient;
        string APIKEY = ConfigurationManager.AppSettings["APIToken"];
        public EndpointsBase()
        {

            restclient = new RestClient("https://cloudpanel-api.1and1.com/v1")
                {
                    Authenticator = new OneAndOneAuthenticator(APIKEY)
                };
        }
    }
}
