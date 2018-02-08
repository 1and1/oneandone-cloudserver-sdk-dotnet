using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OneAndOne.POCO.Converters;
using System.Collections.Generic;
using System;

namespace OneAndOne.POCO.Response.RecoveryAppliances
{
    public class RecoveryApplianceResponse
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "os")]
        public string Os { get; set; }

        private string os_family;
        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(PropertyName = "os_family")]
        public OSFamliyType OsFamily
        {
            get { return (OSFamliyType)Enum.Parse(typeof(OSFamliyType), os_family); }
            set
            {
                os_family = ((OSFamliyType)value).ToString();
            }
        }

        [JsonProperty(PropertyName = "os_version")]
        public string OsVersion { get; set; }

        [JsonProperty(PropertyName = "architecture")]
        public int Architecture { get; set; }

        [JsonConverter(typeof(SingleValueArrayConverter<string>))]
        [JsonProperty(PropertyName = "available_datacenters")]
        public List<string> AvailableDatacenters { get; set; }
    }
}
