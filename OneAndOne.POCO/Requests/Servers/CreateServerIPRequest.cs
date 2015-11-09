using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Requests.Servers
{
    public class CreateServerIPRequest
    {
        private string type;
        [JsonProperty(PropertyName = "type")]
        public IPType Type
        {
            get { return (IPType)Enum.Parse(typeof(IPType), type); }
            set
            {
                type = ((IPType)value).ToString();
            }
        }
    }
    public enum IPType
    {
        IPV4,
        IPV6
    }
}
