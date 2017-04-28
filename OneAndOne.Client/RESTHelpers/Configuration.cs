using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.Client.RESTHelpers
{
    /// <summary>
    /// Represents a set of configuration settings
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// Initializes a new instance of the Configuration class with different settings
        /// </summary>
        /// <param name="apiKey">accessToken</param>
        /// /// <param name="apiUrl">ApiUrl</param>
        public Configuration(string apiKey = null, string apiUrl = null)
        {
            ApiKey = apiKey;
            ApiUrl = apiUrl;
        }

        /// <summary>
        /// Gets or sets the default Configuration.
        /// </summary>
        /// <value>Configuration.</value>
        public static Configuration Default = new Configuration();

        /// <summary>
        /// Gets or sets the access token for OAuth2 authentication.
        /// </summary>
        /// <value>The access token.</value>
        public String ApiKey { get; set; }


        /// <summary>
        /// Gets or sets the api URL
        /// </summary>
        /// <value>The ApiUrl</value>
        public String ApiUrl { get; set; }

    }
}