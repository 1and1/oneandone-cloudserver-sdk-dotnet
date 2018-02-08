using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Runtime.Serialization;

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
    [JsonConverter(typeof(StringEnumConverter))]
    public enum IPType
    {
        [EnumMember(Value = "IPV4")]
        Ipv4,
        [EnumMember(Value = "IPV6")]
        Ipv6
    }
}
