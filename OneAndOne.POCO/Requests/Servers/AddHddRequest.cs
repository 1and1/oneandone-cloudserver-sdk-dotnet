using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Requests.Servers
{
    public class AddHddRequest
    {
        private List<HddRequest> hdds;
        [JsonProperty(PropertyName = "hdds")]

        public List<HddRequest> Hdds
        {
            get { return hdds; }
            set { hdds = value; }
        }
    }
}
