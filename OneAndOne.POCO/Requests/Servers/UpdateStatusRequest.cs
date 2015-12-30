using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Requests.Servers
{
    public class UpdateStatusRequest
    {
        
        private string action;
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(PropertyName = "action")]
        public ServerAction Action
        {
            get { return (ServerAction)Enum.Parse(typeof(ServerAction), action); }
            set
            {
                action = ((ServerAction)value).ToString();
            }
        }
        private string method;
        [JsonProperty(PropertyName = "method")]
        public ServerActionMethod Method
        {
            get { return (ServerActionMethod)Enum.Parse(typeof(ServerActionMethod), method); }
            set
            {
                method = ((ServerActionMethod)value).ToString();
            }
        }

    }
}
