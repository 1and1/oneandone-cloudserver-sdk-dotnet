using Newtonsoft.Json;
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
        [JsonProperty(PropertyName = "action")]
        public string Action
        {
            get { return action; }
            set { action = value; }
        }
        private string method;
        [JsonProperty(PropertyName = "method")]
        public string Method
        {
            get { return method; }
            set { method = value; }
        }

    }
}
