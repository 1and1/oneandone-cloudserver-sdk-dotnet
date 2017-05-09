using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OneAndOne.POCO.Requests.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneAndOne.POCO.Requests.PublicIPs
{
    public class CreatePublicIPRequest
    {
        private string reverse_dns;
        [JsonProperty(PropertyName = "reverse_dns")]
        public string ReverseDns
        {
            get { return reverse_dns; }
            set { reverse_dns = value; }
        }
        private string type;
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(PropertyName = "type")]
        public IPType Type
        {
            get { return (IPType)Enum.Parse(typeof(IPType), type); }
            set
            {
                type = ((IPType)value).ToString();
            }
        }

        ///<summary>
        //datacenter_id 
        //</summary>
        private string datacenter_id;
        [JsonProperty(PropertyName = "datacenter_id")]

        public string DatacenterId
        {
            get
            { return datacenter_id; }

            set
            { datacenter_id = value; }
        }
    }
}
