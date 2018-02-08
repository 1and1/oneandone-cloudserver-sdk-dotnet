using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OneAndOne.POCO.Converters;
using System.Collections.Generic;
using System;

namespace OneAndOne.POCO.Response.RecoveryAppliances
{
    public class RecoveryAppliancesResponse
    {
        public string Id { get; set; }

        public string Name { get; set; }

        [JsonConverter(typeof(SingleValueArrayConverter<string>))]
        [JsonProperty(PropertyName = "available_datacenters")]
        public List<string> AvailableDatacenters { get; set; }

        [JsonProperty(PropertyName = "os")]
        public Os Os { get; set; }
    }

    public class Os
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "family")]
        public string Family { get; set; }

        [JsonProperty(PropertyName = "subfamily")]
        public string Subfamily { get; set; }

        [JsonProperty(PropertyName = "architecture")]
        public int Architecture { get; set; }
    }
}
